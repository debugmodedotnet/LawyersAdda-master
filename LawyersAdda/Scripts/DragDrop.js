function add(v, t, s) {
    var ul = document.getElementById(s);
    var li = document.createElement("li");
    li.appendChild(document.createTextNode(t));
    li.setAttribute("value", v); // added line
    if (s == "target")
        s = "source";
    else
        s = "target";
    li.setAttribute("ondblclick", "add('" + v + "' ,'" + t + "','" + s + "')");
    ul.appendChild(li);
    remove(v, s);
}

function remove(v, s) {
    var ul = document.getElementById(s);
    var elementsLI = ul.getElementsByTagName('li');
    var length = elementsLI.length;
    for (var i = 0; i < length ; ++i) {
        if (elementsLI[i].value == v) {
            ul.removeChild(elementsLI[i]);
        }           
    }
}