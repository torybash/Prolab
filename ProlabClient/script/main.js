var webSocket;
var output = document.getElementById("output");
var ENGINE = {};

//Create the renderer
var renderer = PIXI.autoDetectRenderer(256, 256);

ENGINE.Game = {

    create: function () {

        /* this is called when the state is entered for the very first time */

    },

    step: function (delta) {

    },

    render: function (delta) {

        

    }

};
ENGINE.Staging = {
    stage:null,
    create: function () {
        

        //Add the canvas to the HTML document
        document.body.appendChild(renderer.view);

        //Create a container object called the `stage`
        this.stage = new PIXI.Container();

        
    },
    ready: function() {

    },
    render: function() {
        //Tell the `renderer` to `render` the `stage`
        renderer.render(this.stage);
    },
    keydown: function(event) {
        if(event.key == "enter") {
            console.log("Try to connect");
        }
    }
};

var app = playground({
    width: 640,
    height: 480,
    scale: 1,
    create: function () {

    },
    render: function () {
    },
    ready: function () {
        this.setState(ENGINE.Staging);
    }
});

function connect() {
    webSocket = new WebSocket("ws://192.168.0.106:8080");
    webSocket.onopen = function (event) {
        console.log("connected: " + event.data);
    }

    webSocket.onerror = function (event) {
        console.error("error: " + event.data);
    }

    webSocket.onmessage = function (event) {
        console.log("message: " + event.data);
    }

    webSocket.onclose = function (event) {
        console.log("closed: " + event.data);
        webSocket = null;
    }
}

function disconnect() {
    webSocket.disconnect();
}

function send(data) {
    webSocket.send(data);
}