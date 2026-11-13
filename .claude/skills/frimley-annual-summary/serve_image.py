"""Serve one image on http://127.0.0.1:53917/ with CORS so a script in a Google page can fetch it.

Usage: python -I serve_image.py <path-to-image>   (run in background; stop it when done)
Used to get the header image into the Google Forms header picker (see google_form.md).
"""
import http.server
import sys

PATH = sys.argv[1]


class Handler(http.server.BaseHTTPRequestHandler):
    def _cors(self):
        self.send_header("Access-Control-Allow-Origin", "*")
        self.send_header("Access-Control-Allow-Private-Network", "true")
        self.send_header("Access-Control-Allow-Headers", "*")

    def do_OPTIONS(self):
        self.send_response(204)
        self._cors()
        self.end_headers()

    def do_GET(self):
        data = open(PATH, "rb").read()
        self.send_response(200)
        self._cors()
        self.send_header("Content-Type", "image/jpeg")
        self.send_header("Content-Length", str(len(data)))
        self.end_headers()
        self.wfile.write(data)


http.server.HTTPServer(("127.0.0.1", 53917), Handler).serve_forever()
