const appendExpression = (value) => {
    document.getElementById("expressionInput").value += value;
}

const clearExpression = () => {
    document.getElementById("expressionInput").value = "";
}

const evaluateExpression = () => {
    var expression = document.getElementById("expressionInput").value;

    expression.innerHTML = calculateExpression(expression.value.toString());

    //var xhr = new XMLHttpRequest();
    //xhr.open("POST", "/Calculator/EvaluateExpression", true);
    //xhr.setRequestHeader("Content-Type", "application/json");
    //xhr.onreadystatechange = function () {
    //    if (xhr.readyState === XMLHttpRequest.DONE && xhr.status === 200) {
    //        var result = JSON.parse(xhr.responseText);
    //        document.getElementById("expressionInput").value = result;
    //    }
    //};
    //xhr.send(JSON.stringify({ expression: expression }));
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
    const expression = document.getElementById('expressionInput');
    expression.value = expression.value.slice(0, -1)
}