function AddMessageToView(message, ElementClassNameMess, ProfileId, ElementClassNameImg,MessageId)
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
    imgElement.id = MessageId;
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
function RemoveChilds() {
    var element = document.getElementById("messageConatiner");
    
        while (element.firstChild) {
            element.removeChild(element.firstChild);
        }
    
}
function ChangePhoto(PhotoId, ProfileId) {
    var photo = getElementById(PhotoId);
    photo.src = "/DisplayImage/" + ProfileId;
}