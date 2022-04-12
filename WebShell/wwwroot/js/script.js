let command = document.getElementById("command");
let to_place = document.getElementById("get");
let history = new Array();
let current;
let isActive = false;

async function init() {
    await getCommands();

    document.body.addEventListener("keyup", function(event) {
        switch (event.code) {
            case "Enter":
                if (!isActive) {
                    add();
                }
                break;
            case "ArrowDown":
                if (!isActive) {
                    down();
                }
                break;
            case "ArrowUp":
                if (!isActive) {
                    up();
                }
                break;
        }
    });
}

async function add() {
    if (command.value) {
        var response = await executeCommand();

        var p = document.createElement("p");
        p.classList.add("info");
        var text = document.createTextNode(command.value);
        p.appendChild(text);
        to_place.before(p);
        history.push(command.value);
        current = history.length;
        command.value = "";

        var p_res = document.createElement("p");
        p_res.classList.add("res");

        response.json().then(data => {
            var text_res = document.createTextNode(data);
            p_res.appendChild(text_res);
            to_place.before(p_res);
        });
    }
}

function up() {
    if (history.length > 0 && 0 <= (current - 1) && (current - 1) < history.length) {
        command.value = history[--current];
    }
}

function down() {
    if (history.length > 0 && 0 <= (current + 1) && (current + 1) < history.length) {
        command.value = history[++current];
    }
}

async function getCommands() {
    const response = await fetch('Api/GetCommands', {
        method: 'GET'
    });
    response.json().then(data => {
        current = data.length;
        data.forEach(el => history.push(el.Source));
    });
}

async function executeCommand() {
    isActive = true;
    const response = await fetch('Api/ExecuteCommand', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(command.value)
    });
    isActive = false;
    return response;
}
