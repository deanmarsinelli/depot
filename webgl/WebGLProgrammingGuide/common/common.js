var dir = document.currentScript.src + '/../';

// InitGL will take a canvas element and set up the WebGL context from it
// canvas = html canvas element
// return webgl context
function InitGL(canvas) {

    var gl = canvas.getContext("webgl");
    
    if (!gl) {
        alert('Could not initialize WebGL');
        return null;
    } 

    gl.viewportWidth = canvas.width;
    gl.viewportHeight = canvas.height;
    gl.clearColor(0.0, 0.0, 1.0, 1.0);
    gl.clearDepth(1.0);
    gl.enable(gl.DEPTH_TEST);
    gl.depthFunc(gl.LEQUAL);
    
    return gl;
}


// load a vertex shader from a file
// gl = webgl context
// name = name of the shader source file
// return compiled shader
function LoadVertexShader(gl, name) {
    return LoadShader(gl, gl.VERTEX_SHADER, name);
}


// load a fragment shader from a file
// gl = webgl context
// name = name of the shader source file
// return compiled shader
function LoadFragmentShader(gl, name) {
    return LoadShader(gl, gl.FRAGMENT_SHADER, name);
}


// load a shader from a file
// gl = webgl context
// type = vertex or fragment shader
// name = name of the shader source file
// return compiled shader
function LoadShader(gl, type, name) {

    // load the source from a glsl file
    var extension = '';
    if (type == gl.VERTEX_SHADER) {
        extension = '.vert';
    } else if (type == gl.FRAGMENT_SHADER) {
        extension = '.frag';
    }
    var filePath = dir + 'shaders/' + name + extension;
    var request = new XMLHttpRequest();
    request.open('GET', filePath, false);
    request.send();
    var source = request.responseText;
    if (!source) {
         console.log('Shader file was empty: ' + filePath);
        return null;
    }

    // create shader object
    var shader = gl.createShader(type);
    if (!shader) {
        console.log('Unable to create shader');
        return null;
    }

    // set the shader source and compile the shader
    gl.shaderSource(shader, source);
    gl.compileShader(shader);

    // make sure compilation was successful
    var compiled = gl.getShaderParameter(shader, gl.COMPILE_STATUS);
    if (!compiled) {
        var error = gl.getShaderInfoLog(shader);
        console.log('Failed to compile shader: ' + error);
        gl.deleteShader(shader);
        return null;
    }

    return shader;
}


// create a linked program object (vertex + fragment shader)
// gl = webgl context
// vertexShader = a compiled vertex shader
// fragmentShader = a compiled fragment shader
// return a webgl program
function CreateProgram(gl, vertexShader, fragmentShader) {

    if (!vertexShader || !fragmentShader) {
        console.log('Pass in a null shader');
        return null;
    }

    // create program object
    var program = gl.createProgram();
    if (!program) {
        console.log('Failed to create program object');
        return null;
    }

    // attach shaders to the program and link the program
    gl.attachShader(program, vertexShader);
    gl.attachShader(program, fragmentShader);
    gl.linkProgram(program);

    // ensure the linking was successful
    var linked = gl.getProgramParameter(program, gl.LINK_STATUS);
    if (!linked) {
        var error = gl.getProgramInfoLog(program);
        console.log('Failed to link program: ' + error);
        gl.deleteProgram(program);
        return null;
    }

    // bind shaders to the pipeline
    gl.useProgram(program);

    return program;
}

