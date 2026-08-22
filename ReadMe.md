## 🤖 Advanced AI & Reporting Features

### 1. Auto-Resolution Engine
- **Intent Detection**: Uses Hugging Face (`distilbert-base-uncased`) to detect "Password Reset" intents.
- **Action**: Automatically resolves simple tickets and triggers a secure reset workflow, reducing agent load by ~30%.

### 2. Critical Customer Analytics
- **Trend Analysis**: Identifies customers with **≥2 negative sentiment tickets** in 24 hours.
- **Alerting**: Flags these users as `High Risk` in the database and notifies managers via Slack.

### 3. Real-Time Dashboard (Metabase)
- **Metrics**: 
  - 🕒 Avg Resolution Time by Severity
  - 📊 Ticket Volume by Department
  - ⚠️ Critical Customer List
- **Access**: `http://localhost:3000` (Admin/NoPassword)

### 4. Automated Reporting
- **Formats**: Exports daily summaries to **PDF** (via PDFsharp) and **DOCX** (via MiniWord).
- **Scheduler**: Runs daily at 08:00 UTC; saves to `/reports` and emails stakeholders.

## 🚀 Quick Start
```bash
# Clone & Run
git clone https://github.com/yourname/Project-Gordon.git
cd Project-Gordon
docker-compose up -d

# Access Points
# API: http://localhost:5000
# Dashboard: http://localhost:3000
# Workflows: http://localhost:5678   