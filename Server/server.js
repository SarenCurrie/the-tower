var http = require('http');
var express = require('express');
var mysql = require('mysql');
var underscore = require('underscore');
var logger = require('morgan');

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
  var port = process.env.PORT || 3000;

  app.get('/v1/scores', function (req, res) {
    var total = req.query.total || 10;
    var start = req.query.start || 0;

    function isInt(n) {
      return Number(n) === n && n % 1 === 0;
    }

    if (!isInt(total) || !isInt(start)) {
      res.status(400).send('total and start must be integers');
    }

    connection.query('select * from scores order by score desc limit ? offset ?', [total, start], function (err, rows, fields) {
      if (err) {
        res.status(500).send('Mysql error');
      } else {
        res.send({
          data: rows
        });
      }
    });
  });

  var server = http.createServer(app).listen(port, function() {
    console.log('Listening on port ' + port);
  });
});
