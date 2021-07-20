const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/mainhubchats")
    .build();

hubConnection.on("Send", function (data) {

    let elem = document.createElement("li");
    elem.appendChild(document.createTextNode(data));
    let firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);

});

document.getElementById("sendBtn").addEventListener("click", function (e) {
    let message = document.getElementById("message").value;
    hubConnection.invoke("Send", message);
});

hubConnection.start();