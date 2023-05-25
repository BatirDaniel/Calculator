const appendExpression = (value) => {
    const expression = document.getElementById("expressionInput");

    var regex = /[+\-*/.]/;
    const lastElement = expression.value[expression.value.length - 1];

    if (regex.test(lastElement) && regex.test(value)) {
        expression.value = expression.value.slice(0, -1);
        expression.value += value;
    }
    else {
        expression.value += value;
    }
}

const clearExpression = () => {
    document.getElementById("expressionInput").value = "";
}

const evaluateExpression = () => {
    const expression = document.getElementById("expressionInput").value;

    expression.value = calculateExpression(expression);

    const xhr = new XMLHttpRequest();
    xhr.open("POST", "/Calculator/EvaluateExpression", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            if (xhr.status === 200) {
                const result = JSON.parse(xhr.responseText);
                document.getElementById("expressionInput").value = result;
            } else {
                console.error(xhr.statusText);
                const result = JSON.parse(xhr.responseText);
                document.getElementById("expressionInput").value = result.error;
            }
        }
    };
    xhr.onerror = function () {
        console.error(xhr.statusText);
    };
    xhr.send(JSON.stringify({ expression: expression }));
}

const calculateExpression = (expr) => {
    var total = 0;

    expr = expr.replace(/\s/g, '').match(/[+\-]?([0-9\.\s]+)/g) || [];

    while (expr.length) {
        total += parseFloat(expr.shift());
    }

    return total;
}

const clearLastElement = () => {
    const expression = document.getElementById("expressionInput");
    expression.value = expression.value.slice(0, -1);
}