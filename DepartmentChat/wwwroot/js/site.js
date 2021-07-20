const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/mainhubchats")
    .build();



hubConnection.on("ReceiveMessage", function (userName, message) {
    var blockMessage = html(
        <div class="message message-out">
            <a href="#" data-bs-toggle="modal" data-bs-target="#modal-profile" class="avatar avatar-responsive">
                <img class="avatar-img" src="assets/img/avatars/1.jpg" alt="">
                            </a>

                <div class="message-inner">
                    <div class="message-body">
                        <div class="message-content">
                            <div class="message-text">
                                <p>Hey, Marshall! How are you? Can you please change the color theme of the website to pink and purple? 😂</p>
                            </div>
                        </div>
                    </div>

                    <div class="message-footer">
                        <span class="extra-small text-muted">08:45 PM</span>
                    </div>
                </div>
                        </div>    );
    document.getElementById("chatroom").insertAdjacentHTML('beforeend', "<p>" + userName + ": " + message + "</p>");

});

document.getElementById("sendButton").addEventListener("click", function (e) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var room = document.getElementById("room").value;
    hubConnection.invoke("SendMessage", user, message, room, false);
});

document.getElementById("joinButton").addEventListener("click", function (e) {
    var room = document.getElementById("room").value;
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    hubConnection.invoke("SendMessage", user, message, room, true);
});


hubConnection.start();