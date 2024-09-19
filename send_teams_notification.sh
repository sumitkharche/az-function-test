#!/bin/bash

# Usage: ./send_teams_notification.sh "Message" "Environment" "CST Time" "IST Time" "GitHub Actor" "Pipeline URL" "Webhook URL" "User ID"

message=$1
environment=$2
current_time_cst=$3
current_time_ist=$4
triggered_by=$5
pipeline_url=$6
webhook_url=$7
user_id=$8 # Microsoft Teams User ID (AAD Object ID) to mention

# Send notification to Microsoft Teams
curl -X POST -H "Content-Type: application/json" --data '{
  "type": "message",
  "attachments": [
    {
      "contentType": "application/vnd.microsoft.card.adaptive",
      "content": {
        "type": "AdaptiveCard",
        "version": "1.4",
        "body": [
          {
            "type": "TextBlock",
            "size": "Large",
            "weight": "Bolder",
            "text": "Deployment **STARTED**"
          },
          {
            "type": "FactSet",
            "facts": [
              {
                "title": "**Triggered By:**",
                "value": "'"**${triggered_by}**"'"
              },
              {
                "title": "**Environment:**",
                "value": "'"**${environment}**"'"
              },
              {
                "title": "**Time (CST):**",
                "value": "'"**${current_time_cst}**"'"
              },
              {
                "title": "**Time (IST):**",
                "value": "'"**${current_time_ist}**"'"
              }
            ]
          },
          {
            "type": "ActionSet",
            "actions": [
              {
                "type": "Action.OpenUrl",
                "title": "View Pipeline",
                "url": "'"${pipeline_url}"'"
              }
            ]
          }
        ],
        "msteams": {
          "entities": [
            {
              "type": "mention",
              "text": "<at>'"${user_id}"'</at>",
              "mentioned": {
                "id": "'"${user_id}"'",
                "name": "User" # Replace this with the actual username
              }
            }
          ]
        }
      }
    }
  ]
}' "${webhook_url}"
