function AddMessageToView(message,ElementId)
{
    var Message = document.createElement("p");
    var MessageArea = document.getElementById("messageConatiner");
    Message.textContent = message;
    Message.id = ElementId;
    MessageArea.appendChild(Message);
}