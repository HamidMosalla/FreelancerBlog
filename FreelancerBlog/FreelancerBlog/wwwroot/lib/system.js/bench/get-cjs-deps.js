var fs = require('fs');

// require('...') || exports[''] = ... || exports.asd = ... || module.exports = ...
var cjsExportsRegEx = /(?:^\uFEFF?|[^$_a-zA-Z\xA0-\uFFFF.])(exports\s*(\[['"]|\.)|module(\.exports|\['exports'\]|\["exports"\])\s*(\[['"]|[=,\.]))/;
// RegEx adjusted from https://github.com/jbrantly/yabble/blob/master/lib/yabble.js#L339
var cjsRequireRegEx = /(?:^\uFEFF?|[^$_a-zA-Z\xA0-\uFFFF."'])require\s*\(\s*("[^"\\]*(?:\\.[^"\\]*)*"|'[^'\\]*(?:\\.[^'\\]*)*')\s*\)/g;
var commentRegEx = /(^|[^\\])(\/\*([\s\S]*?)\*\/|([^:]|^)\/\/(.*)$)/mg;

var stringRegEx = /("[^"\\\n\r]*(\\.[^"\\\n\r]*)*"|'[^'\\\n\r]*(\\.[^'\\\n\r]*)*')/g;
function getCJSDeps(source) {
  cjsRequireRegEx.lastIndex = commentRegEx.lastIndex = stringRegEx.lastIndex = 0;

  var deps = [];

  var match;

  // track string and comment locations for unminified source    
  var stringLocations = [], commentLocations = [];

  function inLocation(locations, match) {
    for (var i = 0; i < locations.length; i++)
      if (locations[i][0] < match.index && locations[i][1] > match.index)
        return true;
    return false;
  }

  if (source.length / source.split('\n').length < 200) {
    while (match = stringRegEx.exec(source))
      stringLocations.push([match.index, match.index + match[0].length]);

    // TODO: track template literals here before comments
    
    while (match = commentRegEx.exec(source)) {
      // only track comments not starting in strings
      if (!inLocation(stringLocations, match))
        commentLocations.push([match.index + match[1].length, match.index + match[0].length - 1]);
    }
  }

  while (match = cjsRequireRegEx.exec(source)) {
    // ensure we're not within a string or comment location
    if (!inLocation(stringLocations, match) && !inLocation(commentLocations, match)) {
      var dep = match[1].substr(1, match[1].length - 2);
      // skip cases like require('" + file + "')
      if (dep.match(/"|'/))
        continue;
      // trailing slash requires are removed as they don't map mains in SystemJS
      if (dep[dep.length - 1] == '/')
        dep = dep.substr(0, dep.length - 1);
      deps.push(dep);
    }
  }

  return deps;
}

var cjs = fs.readFileSync('./cjs-sample/cjs.js').toString();

var startTime = Date.now();
for (var i = 0; i < 1000; i++)
  getCJSDeps(cjs);
console.log(Date.now() - startTime);

