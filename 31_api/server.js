const express = require("express");
const bp = require("body-parser");
const port = 8000;
const app = express();
app.use(bp.json());
app.listen(port, () => console.log(`Listening on ${port}`));

app.get('/', (req, res) => {
  res.sendFile(__dirname + '/index.html');
});

app.get('/img/:id', (req, res) => {
  let img = __dirname + "/img/" + req.params.id + ".png";
  res.sendFile(img);
});

app.all('*', (req, res) => {
  const error = {
    errors: 'Route does not exist, please check your link'
  }
  res.status(404).json(error);
})

