# Trawling the Flyers Chat Group for the year's races

Races beyond parkrun (marathons, halves, 10ks, Hart 4, triathlons, the club Handicap) only
appear in the **Flyers Chat Group** on Messenger: https://www.messenger.com/t/988559187867389
Martin must already be logged in to Messenger in Chrome. Use the Claude in Chrome tools
(`navigate`, `javascript_tool`, `browser_batch`, `computer`). All snippets below were used for 2026.

## 1. Open the chat and find the message pane

Navigate to the URL above. Then grab the scroll container (the large `overflow-y:auto` div in
the middle pane; it was the one whose left edge was ~574px at a 2048px-wide viewport, so
check the list if the layout changes):

```js
const els=[...document.querySelectorAll('div')].filter(d=>{const s=getComputedStyle(d);
  return (s.overflowY==='auto'||s.overflowY==='scroll')&&d.scrollHeight>d.clientHeight+50;});
els.map(e=>{const r=e.getBoundingClientRect();return {h:e.scrollHeight,left:Math.round(r.left),w:Math.round(r.width)}})
// then: window.__ffc = <the middle one, widest>
```

Every message has an accessibility line: `Enter, Message sent <date> by <name>: <text>`.
Older messages carry full dates ("17 January 2026, 08:07"); recent ones say "Thursday 14:37".

## 2. Scroll back to 1 January (runs in the page; poll it)

A single awaited loop times out after 45s, so start it without `await` and poll:

```js
window.__msgs = window.__msgs || new Map(); window.__done=false;
(async()=>{const grab=()=>{for (const l of window.__ffc.innerText.split('\n')) if (l.startsWith('Enter, Message sent')) window.__msgs.set(l,true);};
 let stall=0,last=0;
 for (let i=0;i<600;i++){ grab();
   const oldest=window.__ffc.innerText.split('\n').find(l=>l.startsWith('Enter, Message sent'))||'';
   if (/ 2025,/.test(oldest)) break;                       // <- previous year; change each year
   if (window.__msgs.size===last){ if(++stall>15) break; } else {stall=0; last=window.__msgs.size;}
   window.__ffc.scrollTop=0; await new Promise(r=>setTimeout(r,1200)); }
 grab(); window.__done=true;})(); 'started'
```

Poll with a `browser_batch` of several 10-second `wait`s followed by:
`({done:window.__done,count:window.__msgs.size})`. 2026 took ~3 minutes for ~2,100 messages.

## 3. Parse, filter for races, read in small chunks

```js
window.__all=[...window.__msgs.keys()].map(k=>k.match(/^Enter, Message sent (.+?) by ([^:]+): ([\s\S]*)$/)).filter(Boolean)
  .map(m=>({when:m[1],who:m[2],txt:m[3].replace(/https?:\/\/\S+/g,'[link]').replace(/\S+=\S+/g,'').replace(/\s+/g,' ')}));
const re=/marathon|half|\bhm\b|\b\d+\s?k\b|\b\d+\s?km\b|10\s?mile|hart\s?4|ultra|\brace\b|medal|trail|relay|cross country|triathlon|duathlon|\bpb\b|finished|great south|great north|vitality|handicap/i;
window.__hits=window.__all.filter(m=>re.test(m.txt)).map(m=>`${m.when} | ${m.who} | ${m.txt}`);
window.__hits.length
```

- **Strip URLs** (done above) or the tool blocks the output ("Cookie/query string data").
- Output over ~2,000 characters gets truncated: read `window.__hits.slice(a,b).map(l=>l.slice(0,150))`
  ~8 at a time, several reads per `browser_batch`.
- For each race found, read that day's messages to see who actually ran:
  `window.__all.filter(m=>/^(7|8|9) February 2026/.test(m.when)).map(m=>m.who+'|'+m.txt.slice(0,150))`

## 4. Rules for crediting a race

- Only credit someone if the chat says they ran it. Spectating, marshalling, pacing a friend,
  entering a ballot or planning next year's race don't count.
- Messenger shows **first names only** ("Karen", "Paul", "Lou" = Louise McIntosh,
  "Dave" = David Peddle, "Sir Turner of Bobsville" = Bob Turner). Use @mentions, or the
  athletes' parkrun json (who was at parkrun that morning) to disambiguate. In 2026 the
  triathlon "Karen" was Karen Phillips, because Karen Peddle was at parkrun that day.
- Flag anything still uncertain in the notes after the list. Don't guess.
- People not on the roster (family, friends) are listed separately, not in the one-liners.
- Close the tab when finished.

## 2026 for reference

Farnborough Half (Feb), Kingston 20 (Mar), Brighton/Manchester/London Marathons (Apr),
Frimley 10k (May), Yateley 10k and Dorney triathlon (Jun), Flyers Handicap (7 Jun, won by
David Peddle), IOW coastal walk (Jun), Hart 4 relay (Aug), Twilight Challenge (Sep),
Vitality 10k (Sep), Rushmoor Wellesley 10k (Oct), Great South Run and Amsterdam Half (Oct).
