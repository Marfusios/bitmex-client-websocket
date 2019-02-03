workflow "Notification about new PR" {
  on = "pull_request"
  resolves = ["GitHub Action for Slack"]
}

action "GitHub Action for Slack" {
  uses = "Ilshidur/action-slack@305f56a15c09f84b6b4a86e83bff1c41c6b69c63"
  secrets = ["SLACK_WEBHOOK"]
  args = "A new PR has been created in Bitmex websocket repo"
}
