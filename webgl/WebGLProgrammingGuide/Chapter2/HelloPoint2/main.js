
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

    // bind shaders to the pipeline
    gl.useProgram(program);

    // set the position of the vertex
    // assign the data (v0, v1, v2) to the attribute variable specified by location
    gl.vertexAttrib3f(a_Position, 0.0, 0.0, 0.0);
    
    gl.vertexAttrib1f(a_PointSize, 5.0);

    // alternative
    //var position = new Float32Array([0.0, 0.0, 0.0]);
    //gl.vertexAttrib3fv(a_Position, position);

    // clear back buffer (color buffer)
    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    gl.clear(gl.COLOR_BUFFER_BIT);

    gl.drawArrays(gl.POINTS, 0, 1);

}
