"""Crop a photo to a 4:1 Google Forms header (1200x300 jpg).

Usage: python -I crop_header.py <source.jpg> <out.jpg> [topPixel]
topPixel = where the crop band starts (default: centred). For the 2026 trainers photo
(assets/flyers-trainers-original.jpg, 1408x768) top=280 keeps the "Frimley Flyers" lettering.
"""
import sys

from PIL import Image

src, out = sys.argv[1], sys.argv[2]
im = Image.open(src).convert("RGB")
w, h = im.size
band = round(w / 4)
top = int(sys.argv[3]) if len(sys.argv) > 3 else (h - band) // 2
im.crop((0, top, w, top + band)).resize((1200, 300), Image.LANCZOS).save(out, quality=78, optimize=True)
print(f"{src} {w}x{h} -> {out} (crop top {top})")
