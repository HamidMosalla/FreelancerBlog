/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify");

var paths = {
    webroot: "./wwwroot/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:js-custom", function () {
    return gulp.src([
        paths.webroot + "lib/jquery/dist/jquery.js",
        paths.webroot + "lib/bootstrap/dist/js/bootstrap.js"
    ], { base: "." })
                .pipe(concat("mins.js"))
                .pipe(uglify())
                .pipe(gulp.dest(paths.webroot + "/js/mins"));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min:css-custom", function () {
    return gulp.src([
        paths.webroot + "lib/bootstrap/dist/css/bootstrap.css",
        paths.webroot + "lib/bootstrap/dist/css/bootstrap-theme.css"
    ], { base: "." })
        .pipe(concat("mins.css"))
        .pipe(cssmin())
        .pipe(gulp.dest(paths.webroot + "/css/mins"));
});

gulp.task("min", ["min:js", "min:css"]);
