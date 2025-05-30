# Nathan Campus Explorer (Unity Project)

An interactive Unity application developed as part of **Online Project 2** to support navigation and accessibility on Griffith University's Nathan Campus.

---

## Created By — Group: Online Project 2

- Steve Drewery
- Kristal Sapkota
- Zuhayr Azim

---

## Installation Guide

### Prerequisites

- **Unity Hub** (Recommended version: _2022.3 LTS or later_)
- **Unity Editor** (matching version of the project)
- Git (optional, for cloning)
- Windows/Mac with at least 4GB RAM

---

## How to Install the Project

### 1. Clone or Download the Project

Option 1 — Clone via Git:

```bash
git clone https://github.com/your-repo/nathan-campus-explorer.git
```

Option 2 — Download as ZIP:

1. Click the green **Code** button on GitHub → **Download ZIP**
2. Extract the ZIP to a preferred location.

---

## Open the Project in Unity

1. Open **Unity Hub**
2. Click **Add** and navigate to the extracted project folder.
3. Select the folder and click **Open**

---

## Dependencies & Packages

Unity may prompt to install missing packages automatically.  
If not, manually install them:

**Steps:**

- Go to: `Window → Package Manager`
- Install or resolve any missing dependencies

**Required Packages:**

- `TextMeshPro` (For high-quality UI text rendering used in buttons, sliders, labels)
- `NavMesh / AI Navigation` (Built-in Core of the pathfinding and navigation system for the agent.)
- `Line Renderer` (Used for rendering path lines in the minimap and game view)

---

## Running the Application

1. Open the main scene:  
   `Assets/Scenes/OutdoorsScene.unity`
2. Click the **Play ** button in the Unity Editor.
3. Make sure the correct platform is selected:  
   `File → Build Settings → PC or Mac → Set as Target`

---

## Folder Overview

| Folder             | Description                                      |
| ------------------ | ------------------------------------------------ |
| `Assets/`          | Main content, scenes, scripts, models, materials |
| `Packages/`        | Dependency metadata                              |
| `ProjectSettings/` | Unity project settings                           |

---

## Notes

- For best compatibility and performance, it is recommended to use **Unity Editor version 6000.0.41f1**..
- For best performance, close unnecessary apps while running the scene.
- Ensure the minimap camera has a **Render Texture** with a **Depth Buffer** enabled.

---

## Testing Features

- Select navigation paths by choosing start and end buildings from dropdowns.
- Click **Start** to view route animation.
- Toggle between **First Person** and **Third Person** views.
- Evaluate features like **Pause**, **Restart**, and **Accessibility Mode**.

---

## License

This project was developed for educational purposes as part of the **3702ICT coursework at Griffith University**.

---
