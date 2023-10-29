function AddMessageToView(message)
{
    var Message = document.createElement("p");
    var MessageArea = document.getElementById("NWNM");
    Message.textContent = message;
    MessageArea.appendChild(Message);

    console.log("DZIALA ELEGANCKO 420");
}
function TEST()
{
    alert("wdwadwda");
}