# this script nerdyfies any message sent

message = input("Enter the message you wish to nerdify:")

state = False

result = ""

for i in message:
    if state == False:
        state = True
        result += i.lower()
    else:
        state = False
        result += i.capitalize()
    if i == " ":
        result += " :nerd: "

print(result)