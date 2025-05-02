# CoD2 Coloured Name Generator

A console-based tool for generating colorful usernames for **Call of Duty 2**.  
This tool allows you to visually craft names with in-game compatible color codes, experiment with different styles, and output formats.

![Example Output](https://github.com/Collin-Bos/CoD2-Coloured-Name-Generator/blob/18e503d9bd67b129515f615ea7ab298a88ecc5a6/Images/Example1.png)
---

## 💾 How To Install

1. Download the latest release from the [Releases page](https://github.com/Collin-Bos/CoD2-Coloured-Name-Generator/releases)
2. Extract the `.zip`
3. Run the `.exe` file inside

## 💡 How It Works

- Enter a username with **spaces** to indicate new color combinations.
- Use **underscores (`_`)** to create new color segments **without adding spaces** (underscores will be removed in the output).
- Customize output using various commands to control colors, spacing, and formatting.

---

## 🛠️ Commands

| Command                    | Description |
|----------------------------|-------------|
| `/commands`                | Lists all available commands and their descriptions. |
| `/clear`                   | Clears the console output. |
| `/exit`                    | Exits the program. |
| `/exclude <index>`       | Excludes specific color indexes from generation. |
| `/include <index>`       | Includes only the specified color indexes for generation. |
| `/printer <type>`          | Changes the output style. Options: `default`, `cod2` `combined`.<br/> - `default`: What your name will look like in-game.<br/> - `cod2`: Copy-pasteable output for the `/name` command in CoD2.<br/> - `combined`: Both default and cod2 side by side |
| `/settings`                | Displays current settings including active colors. |
| `/colors`                  | Shows all available colors along with their CoD index. |
| `/columns <amount>`        | Sets how many column groups there are. |
| `/whitespace <amount>`     | Sets whitespace spacing between each column group. |
| `/help`                    | Displays a short tutorial on how input formatting works, especially with spaces and underscores. |

---

## 📷 Example


Input: `Xx_Sniper_xX`

Output with `combined` printer:

![Example output2](https://github.com/Collin-Bos/CoD2-Coloured-Name-Generator/blob/744335c95a96dec101f4d62ecfc937c1a44026e3/Images/Example2.png)
