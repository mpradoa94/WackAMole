var data = {},
    dir = "../Data";
fs.readdirSync(dir).forEach(function (file) {
    data[file.replace(/\.json$/, '')] = require(dir + file);
});

console.log(data);