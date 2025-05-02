# CoD2 Coloured Name Generator 🎮🎨

![Example Output](./example-output.png)

A console-based tool for generating colorful usernames for **Call of Duty 2**.  
This tool allows you to visually craft names with in-game compatible color codes, experiment with different styles, and output formats — including copy-paste ready `/name` commands.

---

## 💡 How It Works

- Enter a username with **spaces** to indicate new color combinations.
- Use **underscores (`_`)** to create new color segments **without adding spaces** (underscores will be removed in the output).
- Customize output using various commands to control colors, spacing, and formatting.

---

## 🛠️ Commands

| Command                     | Description |
|----------------------------|-------------|
| `commands`                 | Lists all available commands and their descriptions. |
| `clear`                   | Clears the console output. |
| `exit`                    | Exits the program. |
| `exclude <indexes>`       | Excludes specific color indexes from generation. Example: `exclude 1, 5, 7` |
| `include <indexes>`       | Includes only the specified color indexes for generation. Example: `include 1, 5, 7` |
| `setprinter <type>`       | Changes the output style. Options: `default`, `cod2`.<br/> - `default`: What your name will look like in-game.<br/> - `cod2`: Copy-pasteable output for the `/name` command in CoD2. |
| `settings`                | Displays current settings including active colors and whitespace settings. |
| `colors`                  | Shows all available colors along with their CoD index. |
| `whitespace <amount>`     | Sets whitespace spacing between each column group. |
| `columns <amount>`        | Sets how many column combinations are shown. |
| `help`                    | Displays a short tutorial on how input formatting works, especially with spaces and underscores. |

---

## 📷 Example

```bash
Input: cool _name pro
Output: (shows multi-colored name with default and cod2-style printer outputs)
