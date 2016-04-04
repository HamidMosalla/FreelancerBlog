/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fa';
	// config.uiColor = '#AADC6E';

    config.contentsLangDirection = 'rtl';
    config.height = 450;
    config.extraAllowedContent = '*(*);*{*}';
    //config.extraPlugins = 'popup';
    //config.extraPlugins = 'filebrowser';

    config.filebrowserImageUploadUrl = '/Admin/Portfolio/CkEditorFileUploder';
};
