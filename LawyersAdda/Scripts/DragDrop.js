function add(v, t) {
    var ul = document.getElementById("target");
    var li = document.createElement("li");
    li.appendChild(document.createTextNode(t));
    li.setAttribute("value", v); // added line
    ul.appendChild(li);
    remove(v, "source");
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