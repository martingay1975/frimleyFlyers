# End of Season Google Form

**2026 form (use as next year's template):**
https://docs.google.com/forms/d/1rhSzmT349VubH6UhisHrEB31wfkrgYqKbt9NA_vv-R8/edit
Owned by Martin's Google account; edit it with the Claude in Chrome tools.

## Starting next year's form

1. Open the 2026 form, ⋮ (More) → **Make a copy**, name it "Frimley Flyers End of Season YYYY".
   The copy keeps the questions, theme and header image. Ask Martin before publishing it.
2. Update the year, the date/venue/time of the End of Season Do, and who's providing food.
3. Replace the achievements block (below) with this year's one-liners.
4. Check the "Responses" tab shows 0 responses before deleting or restructuring questions.

## 2026 structure (single page, Submit at the end)

1. Header: title "Frimley Flyers End of Season 🏃‍♂️" + short intro description
2. Your Name: (short answer, required)
3. Name of your Frimley Flyer Of The Year YYYY (short answer, required) —
   description: "Can be your own criteria as long as it is running related. See the bottom
   section for an overview of achievements this year."
4. Reason for choosing this wonderful person (optional) (long answer)
5. Will you be coming to the End Of Season Do? (multiple choice, required) — date/venue in description
6. Food note + "Do you have any dietary requirements?" (short answer)
7. **"Some achievements by you all in YYYY"** — a **Title and description** block (not a
   question, not a separate section) holding the annual-summary one-liners, bold names.

Lesson from 2026: putting the achievements in their own *section* made it page 2, which
respondents only see after answering page 1. Keep it a Title-and-description block on page 1.

## Theme (palette icon, top right)

| Setting | Value |
|---|---|
| Header image | `assets/form-header.jpg` (cropped from `assets/flyers-trainers-original.jpg`) |
| Colour | custom **#2E8B3D** (club green), background: lightest green |
| Header font | **Lobster**, 24 (the maximum) |
| Question font | **Montserrat**, 13 |
| Text font | Roboto, 11 |

A copy of the form keeps all of this; only redo it when starting from scratch.

## Techniques that work in the Forms editor

- **Changing text**: fields are `contenteditable`. Focus the field, select the substring with a
  DOM `Range`, then `document.execCommand('insertText', false, newText)`, then `blur()`.
  Forms saves it ("All changes saved in Drive"). Setting `textContent` directly does NOT save.
- **Pasting formatted (bold) text** into a description: `execCommand('insertHTML')` is blocked
  by Trusted Types. Instead focus the field and dispatch a synthetic paste:
  ```js
  const dt=new DataTransfer(); dt.setData('text/html', html); dt.setData('text/plain', text);
  el.dispatchEvent(new ClipboardEvent('paste',{clipboardData:dt,bubbles:true,cancelable:true}));
  ```
  Build `html` with `<b>Name</b>: …<br><br>` per person, or copy `innerHTML` from an existing field.
- **New block in the right place**: click a card to select it, then the toolbar's "Tt"
  (Add title and description) inserts the block directly after it.
- **Header image upload**: the header picker's Upload tab is a same-origin iframe
  (`https://docs.google.com/picker`) whose `input[type=file]` the Chrome tools can't target.
  1. `python -I crop_header.py assets/flyers-trainers-original.jpg <scratch>/header.jpg 280`
  2. Run `python -I serve_image.py <scratch>/header.jpg` in the background.
  3. Theme → Header → Choose image → Upload, then in the page:
     ```js
     const blob=await fetch('http://127.0.0.1:53917/x.jpg').then(r=>r.blob());
     const fr=[...document.querySelectorAll('iframe')].find(f=>(f.src||'').startsWith('https://docs.google.com/picker'));
     const input=fr.contentDocument.querySelector('input[type=file]');
     const dt=new (fr.contentWindow.DataTransfer)();
     dt.items.add(new (fr.contentWindow.File)([blob],'header.jpg',{type:'image/jpeg'}));
     input.files=dt.files; input.dispatchEvent(new fr.contentWindow.Event('change',{bubbles:true}));
     ```
  4. Click **Save** on "Position header", then stop the server.
  Forms then auto-picks a colour from the photo: set the custom green again afterwards.
- **Verify** in the respondent view (`.../viewform`): `window.FB_PUBLIC_LOAD_DATA_` holds every
  item on every page, so you can confirm content without filling in required fields.
