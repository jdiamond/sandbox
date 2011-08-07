$(function() {
    var $scripts = $('script'),
        setUp = $scripts.filter('[data-set-up]').data('setUp'),
        tearDown = $scripts.filter('[data-tear-down]').data('tearDown'),
        results = {},
        reporters = {};

    $scripts.filter('[data-callbacks]').each(function() {
        $.each(split($(this).data('callbacks')), function(i, type) {
            if (!reporters[type]) {
                reporters[type] = window[$scripts.filter('[data-report-' + type + ']').data('report-' + type)];
            }
        });
    });

    if (setUp && window[setUp]) {
        window[setUp]();
    }

    $('[data-example]').each(function() {
        var $section = $(this),
            name = $section.data('example'),
            $script = $section.find('script', $section),
            argNames = split($script.data('args')),
            resultName = $script.data('result'),
            callbacks = split($script.data('callbacks')),
            async = $script.data('async'),
            $buttons = $('<p>').appendTo($section),
            tables = {};

        $('<button>Go</button>')
            .appendTo($buttons)
            .click(function(e) {
                var args = $.map(argNames, getArg);

                $.each(callbacks, function(i, type) {
                    args.push(function(data) {
                        reporters[type](tables[type][0], data);
                    });
                });

                if (async) {
                    args.push(function(result) {
                        if (resultName) {
                            results[resultName] = result;
                        }
                        console.log(name + ' succeeded');
                    });
                    args.push(function() {
                        alert(name + ' failed =(');
                    });
                }

                window[name].apply(window, args);
            });

        if (callbacks.length) {
            $('<button>Clear</button>')
                .appendTo($buttons)
                .click(function(e) {
                    $.each(tables, function(i, $table) {
                        $table.empty();
                    });
                });
        }

        $('<button>View Source</button>')
            .appendTo($buttons)
            .click(function(e) {
                var $button = $(this);
                if (!$button.data('source')) {
                    $button.data('source', $('<pre>').text($script.text())
                                                     .appendTo($section));
                    $button.text('Hide Source');
                } else {
                    $button.data('source').remove();
                    $button.removeData('source');
                    $button.text('View Source');
                }
            });

        $.each(callbacks, function(i, type) {
            tables[type] = $('<table>').appendTo($section);
        });

        function getArg(name) {
            var arg = results[name];
            var $elem = $('[name="' + name + '"]', $section);
            if (!$elem.length) {
                if (!arg) throw 'missing ' + name;
                return arg;
            }
            if ($elem.is('input:checkbox')) return $elem.prop('checked');
            if ($elem.prop('type') === 'number') return parseInt($elem.val(), 10);
            return $elem.val();
        }
    });

    function split(s) {
        if (!s) return [];
        return s.split(' ');
    }

    window.onunload = function() {
        if (tearDown && window[tearDown]) {
            window[tearDown](results);
        }
    };

});

