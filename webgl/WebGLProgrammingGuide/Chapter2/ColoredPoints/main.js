var g_points = [];
var g_colors = [];

function click(event, gl, canvas, a_Position, u_FragColor) {
    var x = event.clientX;
    var y = event.clientY;
    var rect = event.target.getBoundingClientRect();

    // transform the x, y coords from 'client space' to 'canvas space' to 'world space'
    x = ((x - rect.left) - (canvas.width / 2)) / (canvas.width / 2);
    y = ((canvas.height / 2) - (y - rect.top)) / (canvas.height / 2);
    
    // push the coordinates as a pair
    g_points.push([x, y]);

    // color the point based on the position value
    if (x >= 0.0 && y >= 0.0) {
        g_colors.push([1.0, 0.0, 0.0, 1.0]);
    } else if (x < 0.0 && y < 0.0) {
        g_colors.push([0.0, 1.0, 0.0, 1.0]);
    } else {
        g_colors.push([0.0, 0.0, 1.0, 1.0]);
    }

    gl.clear(gl.COLOR_BUFFER_BIT);

    var len = g_points.length;
    for (var i = 0; i < len; i++) {
        var color = g_colors[i];
        var point = g_points[i];

        // pass position to the attribute variable
        gl.vertexAttrib3f(a_Position, point[0], point[1], 0.0);
        // pass color to the uniform variable
        gl.uniform4f(u_FragColor, color[0], color[1], color[2], color[3]);
        // draw
        gl.drawArrays(gl.POINTS, 0 , 1);
    }
}

function main() {
    var canvas = document.getElementById('webgl');

    if (!canvas) {
        console.log('Failed to retrieve canvas element');
        return;
    }

    var gl = InitGL(canvas);
    if (!gl) {
        return;
    }

    var vertexShader = LoadVertexShader(gl, 'HelloPoint2');
    var fragmentShader = LoadFragmentShader(gl, 'ColoredPoints');
    if (!vertexShader || !fragmentShader) {
        console.log('One of the shaders failed to load');
        return;
    }

    var program = CreateProgram(gl, vertexShader, fragmentShader);
    if (!program) {
        console.log('Program failed to link');
        return;
    }


    var a_Position = gl.getAttribLocation(program, 'a_Position');
    var a_PointSize = gl.getAttribLocation(program, 'a_PointSize');
    var u_FragColor = gl.getUniformLocation(program, 'u_FragColor');

    gl.vertexAttrib1f(a_PointSize, 10.0);

    // bind shaders to the pipeline
    gl.useProgram(program);
    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    gl.clear(gl.COLOR_BUFFER_BIT);

    canvas.onmousedown = function(event) { click(event, gl, canvas, a_Position, u_FragColor); };
}
