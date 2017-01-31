var g_points = [];

function click(event, gl, canvas, a_Position) {
    var x = event.clientX;
    var y = event.clientY;
    var rect = event.target.getBoundingClientRect();

    x = ((x - rect.left) - (canvas.width / 2)) / (canvas.width / 2);
    y = ((canvas.height / 2) - (y - rect.top)) / (canvas.height / 2);
    
    g_points.push(x);
    g_points.push(y);

    gl.clear(gl.COLOR_BUFFER_BIT);

    var len = g_points.length;
    for (var i = 0; i < len; i += 2) {
        gl.vertexAttrib3f(a_Position, g_points[i], g_points[i + 1], 0.0);
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
    var fragmentShader = LoadFragmentShader(gl, 'HelloPoint1'); // share the same shader as HelloPoint1
    if (!vertexShader || !fragmentShader) {
        console.log('One of the shaders failed to load');
        return;
    }

    var program = CreateProgram(gl, vertexShader, fragmentShader);
    if (!program) {
        console.log('Program failed to link');
        return;
    }

    // get attribute variable 'a_Position'
    var a_Position = gl.getAttribLocation(program, 'a_Position');
    if (a_Position < 0) {
        console.log('Failed to get the storage location for a_Position');
        return;
    }
    var a_PointSize = gl.getAttribLocation(program, 'a_PointSize');
    if (a_PointSize < 0) {
        console.log('Failed to get the storage location for a_Position');
        return;
    }

    gl.vertexAttrib1f(a_PointSize, 25.0);

    // bind shaders to the pipeline
    gl.useProgram(program);
    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    gl.clear(gl.COLOR_BUFFER_BIT);

    canvas.onmousedown = function(event) { click(event, gl, canvas, a_Position); };
}
