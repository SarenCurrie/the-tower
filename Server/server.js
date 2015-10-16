var http = require('http');
var express = require('express');
var bodyParser = require('body-parser');
var mysql = require('mysql');

var connection = mysql.createConnection({
  host: 'localhost',
  user: '', // insert mysql username here
  password: '', // isert mysql password here
  database: 'theTower'
});

connection.connect(function(err) {
  if (err) {
    console.error('error connecting: ' + err.stack);
    return;
  }

  console.log('connected as id ' + connection.threadId);

  // Set up application.
  var app = express();
  var port = process.env.PORT || 80;

  app.use(bodyParser.json());

  app.use(express.static('public'));

  app.get('/v1/scores', function (req, res) {
    var total = req.query.total || 10;
    var start = req.query.start || 0;

    total = parseInt(total);
    start = parseInt(start);

    if (isNaN(total) || isNaN(start)) {
      res.status(400).send('total and start must be integers');
      return;
    }

    connection.query('select * from scores order by score desc limit ? offset ?', [total, start], function (err, rows, fields) {
      if (err) {
        res.status(500).send('Mysql error');
      } else {
        res.status(200).send({
          data: rows
        });
      }
    });
  });

  app.post('/v1/scores', function (req, res) {
    var playerName = req.body.name;
    var score = req.body.score;

    if (!playerName || !score) {
      res.status(400).send('missing value');
      return;
    }

    score = parseInt(score);

    if (isNaN(score)) {
      res.send('score must be an integer');
      return;
    }

    connection.query('insert into scores (name, score) values(?, ?)', [playerName, score], function (err, rows, fields) {
      if (err) {
        res.status(500).send('Mysql error: ' + err);
      } else {
        res.status(200).send();
      }
    });
  });

  var server = http.createServer(app).listen(port, function() {
    console.log('Listening on port ' + port);
  });
});
