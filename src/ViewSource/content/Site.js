/// <reference path="~/Libraries/jquery-1.8.3.min.js"/>
/// <reference path="~/Libraries/beautify-html.js"/>

(function(window, document, undefined) {
    'use strict';

    var $url, $sourceText, $nowrap, $beautify;
    $url = $('#url');

    var isResultPage = $('body').hasClass('result');
    var isHomePage = $('body').hasClass('home');

    if (isHomePage) {
        $url.select().focus();
        return;
    } else if (!isResultPage) {
        return;
    }
    
    $sourceText = $('#source-text');
    $nowrap = $('#nowrap');
    $beautify = $('#beautify');

    var viewSource = {
        initEncoding: function() {
            $('#encoding').change(function() {
                $(this).attr('readonly', 'readonly');
                $('form').submit();
            });
        },

        initNowrap: function() {
            $nowrap.click(function() {
                var checked = $(this).is(':checked');
                $sourceText.toggleClass('nowrap', checked);
                $sourceText.attr('wrap', function() {
                    return checked ? 'off' : null;
                });
            });
        },

        isSourceTextHeightAdjusted: false,
        initSourceTextHeight: function() {
            var sourceHeight = $sourceText.height();
            var windowHeight = $(window).height();
            if (sourceHeight > windowHeight) {
                $sourceText.height(windowHeight - 10);
                viewSource.isSourceTextHeightAdjusted = true;
            }
        },

        initBeautify: function() {
            var originalSource = $sourceText.val();

            var isHtml = viewSource.looksLikeHtml(originalSource);
            if (isHtml) {
                viewSource.showHtml(originalSource);
            } else {
                viewSource.showNonHtml(originalSource);
            }
        },
        looksLikeHtml: function(source) {
            var trimmed = source.replace(/^[ \t\n\r]+/, '');
            var commentMark = '<' + '!-' + '-';
            return (trimmed && (trimmed.substring(0, 1) === '<' && trimmed.substring(0, 4) !== commentMark));
        },
        showHtml: function(originalSource) {
            var beautifiedSource = style_html(originalSource);

            $sourceText.val(beautifiedSource);

            $beautify.click(function() {
                var checked = $(this).is(':checked');
                var source = checked ? beautifiedSource : originalSource;
                $sourceText.val(source);
            });
        },
        showNonHtml: function(originalSource) {
            $('#beautify').parent().hide();
            $sourceText.val(originalSource);
        },

        initSourceSelect: function() {
            $sourceText.on('click', function() {
                $sourceText.select();
            });
            $sourceText.keydown(function(e) {
                e.preventDefault();
                e.stopPropagation();
                return false;
            });
        }
    };

    viewSource.initEncoding();
    viewSource.initNowrap();
    viewSource.initSourceTextHeight();
    viewSource.initBeautify();
    viewSource.initSourceSelect();

    if (viewSource.isSourceTextHeightAdjusted && $sourceText.length) {
        var scrollDestination = ($sourceText.offset().top - 2);
        $('html, body').animate({
            scrollTop: scrollDestination
        }, 500);
    } else {
        setTimeout(function () {
            if ($(document).scrollTop() < 1) {
                window.scrollTo(0, 1);
            }
        }, 1000);
    }
}(window, document));