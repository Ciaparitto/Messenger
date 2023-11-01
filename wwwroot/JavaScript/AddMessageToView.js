function AddMessageToView(message, ElementClassName)
{
    var Message = document.createElement("li");
    var MessageArea = document.getElementById("messageConatiner");
    Message.innerHTML = message;
    Message.className = ElementClassName;
    MessageArea.appendChild(Message);
    scrollToBottom();
}
function scrollToBottom() {
    var div = document.getElementById("messageConatiner");
    div.scrollTop = div.scrollHeight;
}
