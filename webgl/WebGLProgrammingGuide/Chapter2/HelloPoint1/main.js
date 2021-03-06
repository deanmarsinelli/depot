
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

    var vertexShader = LoadVertexShader(gl, 'HelloPoint1');
    var fragmentShader = LoadFragmentShader(gl, 'HelloPoint1');
    var program = CreateProgram(gl, vertexShader, fragmentShader);

    // bind shaders to the pipeline
    gl.useProgram(program);

    // clear back buffer (color buffer)
    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    gl.clear(gl.COLOR_BUFFER_BIT);

    gl.drawArrays(gl.POINTS, 0, 1);
}
