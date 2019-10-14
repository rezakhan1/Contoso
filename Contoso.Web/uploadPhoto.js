angular.module('contosoApp').directive("contosoFiles", function($parse) {
    return function link(scope, element, attrs) {
        var onChange = $parse(attrs.contosoFiles);

        element.on("change", function(event) {
            onChange(scope, { $files: event.target.files });
        });
    }
});