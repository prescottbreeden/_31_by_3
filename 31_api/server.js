const express = require("express")
const app = express()

const bp = require("body-parser")
const path = require("path")


app.use(express.static(path.join(__dirname + '/client/dist')))


app.get('/img/:id', function(req, res){
    let img = __dirname + "/img/" + req.params.id + ".png"
    res.sendFile(img)
})

app.use(bp.json())

app.listen(8000, function(){
    console.log("Listening on 8000")
})