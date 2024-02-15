function AddMessageToView(message, ElementClassNameMess, ProfileId, ElementClassNameImg)
{
    var MessageImgContainer = document.createElement("div");
    var ImgContainer = document.createElement("div");
    var MessContainer = document.createElement("div");
    var MessageContainerChild = document.createElement("div");

  
    

    MessageContainerChild.className = "MessageContainerChild" + ElementClassNameMess;
    MessageImgContainer.className = ElementClassNameMess + "Container";
    var MessageArea = document.getElementById("messageConatiner");
    MessageArea.appendChild(MessageImgContainer);
    MessageImgContainer.appendChild(MessageContainerChild);
    var Message = document.createElement("div");
    Message.innerHTML = message;
    MessContainer.className = ElementClassNameMess;
   
    MessageContainerChild.appendChild(MessContainer);
    MessageContainerChild.appendChild(ImgContainer);

    var imgElement = document.createElement("img");
    imgElement.src = "/DisplayImage/" + ProfileId;
    ImgContainer.className = ElementClassNameImg;
    imgElement.className = "photo";
 
    MessContainer.appendChild(Message);
    ImgContainer.appendChild(imgElement);
   
    
 
 
    scrollToBottom();
}
function scrollToBottom() {
    var div = document.getElementById("messageConatiner");
    div.scrollTop = div.scrollHeight;
}