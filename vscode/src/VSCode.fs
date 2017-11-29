// ts2fable 0.5.2
module rec VSCode
open System
open Fable.Core
open Fable.Import.JS

let [<Import("*","vscode")>] vscode: Vscode.IExports = jsNative

module Vscode =
    let [<Import("env","vscode")>] env: Env.IExports = jsNative
    let [<Import("commands","vscode")>] commands: Commands.IExports = jsNative
    let [<Import("window","vscode")>] window: Window.IExports = jsNative
    let [<Import("workspace","vscode")>] workspace: Workspace.IExports = jsNative
    let [<Import("languages","vscode")>] languages: Languages.IExports = jsNative
    let [<Import("scm","vscode")>] scm: Scm.IExports = jsNative
    let [<Import("debug","vscode")>] debug: Debug.IExports = jsNative
    let [<Import("extensions","vscode")>] extensions: Extensions.IExports = jsNative

    type [<AllowNullLiteral>] IExports =
        abstract version: string
        abstract Position: PositionStatic
        abstract Range: RangeStatic
        abstract Selection: SelectionStatic
        abstract ThemeColor: ThemeColorStatic
        abstract Uri: UriStatic
        abstract CancellationTokenSource: CancellationTokenSourceStatic
        abstract Disposable: DisposableStatic
        abstract EventEmitter: EventEmitterStatic
        abstract RelativePattern: RelativePatternStatic
        abstract CodeLens: CodeLensStatic
        abstract MarkdownString: MarkdownStringStatic
        abstract Hover: HoverStatic
        abstract DocumentHighlight: DocumentHighlightStatic
        abstract SymbolInformation: SymbolInformationStatic
        abstract TextEdit: TextEditStatic
        abstract WorkspaceEdit: WorkspaceEditStatic
        abstract SnippetString: SnippetStringStatic
        abstract ParameterInformation: ParameterInformationStatic
        abstract SignatureInformation: SignatureInformationStatic
        abstract SignatureHelp: SignatureHelpStatic
        abstract CompletionItem: CompletionItemStatic
        abstract CompletionList: CompletionListStatic
        abstract DocumentLink: DocumentLinkStatic
        abstract Color: ColorStatic
        abstract ColorInformation: ColorInformationStatic
        abstract ColorPresentation: ColorPresentationStatic
        abstract Location: LocationStatic
        abstract Diagnostic: DiagnosticStatic
        abstract TaskGroup: TaskGroupStatic
        abstract ProcessExecution: ProcessExecutionStatic
        abstract ShellExecution: ShellExecutionStatic
        abstract Task: TaskStatic
        abstract TreeItem: TreeItemStatic

    /// Represents a reference to a command. Provides a title which
    /// will be used to represent a command in the UI and, optionally,
    /// an array of arguments which will be passed to the command handler
    /// function when invoked.
    type [<AllowNullLiteral>] Command =
        /// Title of the command, like `save`.
        abstract title: string with get, set
        /// The identifier of the actual command handler.
        abstract command: string with get, set
        /// A tooltip for for command, when represented in the UI.
        abstract tooltip: string option with get, set
        /// Arguments that the command handler should be
        /// invoked with.
        abstract arguments: ResizeArray<obj option> option with get, set

    /// Represents a line of text, such as a line of source code.
    /// 
    /// TextLine objects are __immutable__. When a [document](#TextDocument) changes,
    /// previously retrieved lines will not represent the latest state.
    type [<AllowNullLiteral>] TextLine =
        /// The zero-based line number.
        abstract lineNumber: float
        /// The text of this line without the line separator characters.
        abstract text: string
        /// The range this line covers without the line separator characters.
        abstract range: Range
        /// The range this line covers with the line separator characters.
        abstract rangeIncludingLineBreak: Range
        /// The offset of the first character which is not a whitespace character as defined
        /// by `/\s/`. **Note** that if a line is all whitespaces the length of the line is returned.
        abstract firstNonWhitespaceCharacterIndex: float
        /// Whether this line is whitespace only, shorthand
        /// for [TextLine.firstNonWhitespaceCharacterIndex](#TextLine.firstNonWhitespaceCharacterIndex) === [TextLine.text.length](#TextLine.text).
        abstract isEmptyOrWhitespace: bool

    /// Represents a text document, such as a source file. Text documents have
    /// [lines](#TextLine) and knowledge about an underlying resource like a file.
    type [<AllowNullLiteral>] TextDocument =
        /// The associated URI for this document. Most documents have the __file__-scheme, indicating that they
        /// represent files on disk. However, some documents may have other schemes indicating that they are not
        /// available on disk.
        abstract uri: Uri
        /// The file system path of the associated resource. Shorthand
        /// notation for [TextDocument.uri.fsPath](#TextDocument.uri). Independent of the uri scheme.
        abstract fileName: string
        /// Is this document representing an untitled file.
        abstract isUntitled: bool
        /// The identifier of the language associated with this document.
        abstract languageId: string
        /// The version number of this document (it will strictly increase after each
        /// change, including undo/redo).
        abstract version: float
        /// `true` if there are unpersisted changes.
        abstract isDirty: bool
        /// `true` if the document have been closed. A closed document isn't synchronized anymore
        /// and won't be re-used when the same resource is opened again.
        abstract isClosed: bool
        /// Save the underlying file.
        abstract save: unit -> Thenable<bool>
        /// The [end of line](#EndOfLine) sequence that is predominately
        /// used in this document.
        abstract eol: EndOfLine
        /// The number of lines in this document.
        abstract lineCount: float
        /// <summary>Returns a text line denoted by the line number. Note
        /// that the returned object is *not* live and changes to the
        /// document are not reflected.</summary>
        /// <param name="line">A line number in [0, lineCount).</param>
        abstract lineAt: line: float -> TextLine
        /// <summary>Returns a text line denoted by the position. Note
        /// that the returned object is *not* live and changes to the
        /// document are not reflected.
        /// 
        /// The position will be [adjusted](#TextDocument.validatePosition).</summary>
        /// <param name="position">A position.</param>
        abstract lineAt: position: Position -> TextLine
        /// <summary>Converts the position to a zero-based offset.
        /// 
        /// The position will be [adjusted](#TextDocument.validatePosition).</summary>
        /// <param name="position">A position.</param>
        abstract offsetAt: position: Position -> float
        /// <summary>Converts a zero-based offset to a position.</summary>
        /// <param name="offset">A zero-based offset.</param>
        abstract positionAt: offset: float -> Position
        /// <summary>Get the text of this document. A substring can be retrieved by providing
        /// a range. The range will be [adjusted](#TextDocument.validateRange).</summary>
        /// <param name="range">Include only the text included by the range.</param>
        abstract getText: ?range: Range -> string
        /// <summary>Get a word-range at the given position. By default words are defined by
        /// common separators, like space, -, _, etc. In addition, per languge custom
        /// [word definitions](#LanguageConfiguration.wordPattern) can be defined. It
        /// is also possible to provide a custom regular expression.
        /// 
        /// * *Note 1:* A custom regular expression must not match the empty string and
        /// if it does, it will be ignored.
        /// * *Note 2:* A custom regular expression will fail to match multiline strings
        /// and in the name of speed regular expressions should not match words with
        /// spaces. Use [`TextLine.text`](#TextLine.text) for more complex, non-wordy, scenarios.
        /// 
        /// The position will be [adjusted](#TextDocument.validatePosition).</summary>
        /// <param name="position">A position.</param>
        /// <param name="regex">Optional regular expression that describes what a word is.</param>
        abstract getWordRangeAtPosition: position: Position * ?regex: RegExp -> Range option
        /// <summary>Ensure a range is completely contained in this document.</summary>
        /// <param name="range">A range.</param>
        abstract validateRange: range: Range -> Range
        /// <summary>Ensure a position is contained in the range of this document.</summary>
        /// <param name="position">A position.</param>
        abstract validatePosition: position: Position -> Position

    /// Represents a line and character position, such as
    /// the position of the cursor.
    /// 
    /// Position objects are __immutable__. Use the [with](#Position.with) or
    /// [translate](#Position.translate) methods to derive new positions
    /// from an existing position.
    type [<AllowNullLiteral>] Position =
        /// The zero-based line value.
        abstract line: float
        /// The zero-based character value.
        abstract character: float
        /// <summary>Check if `other` is before this position.</summary>
        /// <param name="other">A position.</param>
        abstract isBefore: other: Position -> bool
        /// <summary>Check if `other` is before or equal to this position.</summary>
        /// <param name="other">A position.</param>
        abstract isBeforeOrEqual: other: Position -> bool
        /// <summary>Check if `other` is after this position.</summary>
        /// <param name="other">A position.</param>
        abstract isAfter: other: Position -> bool
        /// <summary>Check if `other` is after or equal to this position.</summary>
        /// <param name="other">A position.</param>
        abstract isAfterOrEqual: other: Position -> bool
        /// <summary>Check if `other` equals this position.</summary>
        /// <param name="other">A position.</param>
        abstract isEqual: other: Position -> bool
        /// <summary>Compare this to `other`.</summary>
        /// <param name="other">A position.</param>
        abstract compareTo: other: Position -> float
        /// <summary>Create a new position relative to this position.</summary>
        /// <param name="lineDelta">Delta value for the line value, default is `0`.</param>
        /// <param name="characterDelta">Delta value for the character value, default is `0`.</param>
        abstract translate: ?lineDelta: float * ?characterDelta: float -> Position
        /// <summary>Derived a new position relative to this position.</summary>
        /// <param name="change">An object that describes a delta to this position.</param>
        abstract translate: change: PositionTranslateChange -> Position
        /// <summary>Create a new position derived from this position.</summary>
        /// <param name="line">Value that should be used as line value, default is the [existing value](#Position.line)</param>
        /// <param name="character">Value that should be used as character value, default is the [existing value](#Position.character)</param>
        abstract ``with``: ?line: float * ?character: float -> Position
        /// <summary>Derived a new position from this position.</summary>
        /// <param name="change">An object that describes a change to this position.</param>
        abstract ``with``: change: PositionWithChange -> Position

    type [<AllowNullLiteral>] PositionTranslateChange =
        abstract lineDelta: float option with get, set
        abstract characterDelta: float option with get, set

    type [<AllowNullLiteral>] PositionWithChange =
        abstract line: float option with get, set
        abstract character: float option with get, set

    /// Represents a line and character position, such as
    /// the position of the cursor.
    /// 
    /// Position objects are __immutable__. Use the [with](#Position.with) or
    /// [translate](#Position.translate) methods to derive new positions
    /// from an existing position.
    type [<AllowNullLiteral>] PositionStatic =
        /// <param name="line">A zero-based line value.</param>
        /// <param name="character">A zero-based character value.</param>
        [<Emit "new $0($1...)">] abstract Create: line: float * character: float -> Position

    /// A range represents an ordered pair of two positions.
    /// It is guaranteed that [start](#Range.start).isBeforeOrEqual([end](#Range.end))
    /// 
    /// Range objects are __immutable__. Use the [with](#Range.with),
    /// [intersection](#Range.intersection), or [union](#Range.union) methods
    /// to derive new ranges from an existing range.
    type [<AllowNullLiteral>] Range =
        /// The start position. It is before or equal to [end](#Range.end).
        abstract start: Position
        /// The end position. It is after or equal to [start](#Range.start).
        abstract ``end``: Position
        /// `true` if `start` and `end` are equal.
        abstract isEmpty: bool with get, set
        /// `true` if `start.line` and `end.line` are equal.
        abstract isSingleLine: bool with get, set
        /// <summary>Check if a position or a range is contained in this range.</summary>
        /// <param name="positionOrRange">A position or a range.</param>
        abstract contains: positionOrRange: U2<Position, Range> -> bool
        /// <summary>Check if `other` equals this range.</summary>
        /// <param name="other">A range.</param>
        abstract isEqual: other: Range -> bool
        /// <summary>Intersect `range` with this range and returns a new range or `undefined`
        /// if the ranges have no overlap.</summary>
        /// <param name="range">A range.</param>
        abstract intersection: range: Range -> Range option
        /// <summary>Compute the union of `other` with this range.</summary>
        /// <param name="other">A range.</param>
        abstract union: other: Range -> Range
        /// <summary>Derived a new range from this range.</summary>
        /// <param name="start">A position that should be used as start. The default value is the [current start](#Range.start).</param>
        /// <param name="end">A position that should be used as end. The default value is the [current end](#Range.end).</param>
        abstract ``with``: ?start: Position * ?``end``: Position -> Range
        /// <summary>Derived a new range from this range.</summary>
        /// <param name="change">An object that describes a change to this range.</param>
        abstract ``with``: change: RangeWithChange -> Range

    type [<AllowNullLiteral>] RangeWithChange =
        abstract start: Position option with get, set
        abstract ``end``: Position option with get, set

    /// A range represents an ordered pair of two positions.
    /// It is guaranteed that [start](#Range.start).isBeforeOrEqual([end](#Range.end))
    /// 
    /// Range objects are __immutable__. Use the [with](#Range.with),
    /// [intersection](#Range.intersection), or [union](#Range.union) methods
    /// to derive new ranges from an existing range.
    type [<AllowNullLiteral>] RangeStatic =
        /// <summary>Create a new range from two positions. If `start` is not
        /// before or equal to `end`, the values will be swapped.</summary>
        /// <param name="start">A position.</param>
        /// <param name="end">A position.</param>
        [<Emit "new $0($1...)">] abstract Create: start: Position * ``end``: Position -> Range
        /// <summary>Create a new range from number coordinates. It is a shorter equivalent of
        /// using `new Range(new Position(startLine, startCharacter), new Position(endLine, endCharacter))`</summary>
        /// <param name="startLine">A zero-based line value.</param>
        /// <param name="startCharacter">A zero-based character value.</param>
        /// <param name="endLine">A zero-based line value.</param>
        /// <param name="endCharacter">A zero-based character value.</param>
        [<Emit "new $0($1...)">] abstract Create: startLine: float * startCharacter: float * endLine: float * endCharacter: float -> Range

    /// Represents a text selection in an editor.
    type [<AllowNullLiteral>] Selection =
        inherit Range
        /// The position at which the selection starts.
        /// This position might be before or after [active](#Selection.active).
        abstract anchor: Position with get, set
        /// The position of the cursor.
        /// This position might be before or after [anchor](#Selection.anchor).
        abstract active: Position with get, set
        /// A selection is reversed if [active](#Selection.active).isBefore([anchor](#Selection.anchor)).
        abstract isReversed: bool with get, set

    /// Represents a text selection in an editor.
    type [<AllowNullLiteral>] SelectionStatic =
        /// <summary>Create a selection from two postions.</summary>
        /// <param name="anchor">A position.</param>
        /// <param name="active">A position.</param>
        [<Emit "new $0($1...)">] abstract Create: anchor: Position * active: Position -> Selection
        /// <summary>Create a selection from four coordinates.</summary>
        /// <param name="anchorLine">A zero-based line value.</param>
        /// <param name="anchorCharacter">A zero-based character value.</param>
        /// <param name="activeLine">A zero-based line value.</param>
        /// <param name="activeCharacter">A zero-based character value.</param>
        [<Emit "new $0($1...)">] abstract Create: anchorLine: float * anchorCharacter: float * activeLine: float * activeCharacter: float -> Selection

    type [<RequireQualifiedAccess>] TextEditorSelectionChangeKind =
        | Keyboard = 1
        | Mouse = 2
        | Command = 3

    /// Represents an event describing the change in a [text editor's selections](#TextEditor.selections).
    type [<AllowNullLiteral>] TextEditorSelectionChangeEvent =
        /// The [text editor](#TextEditor) for which the selections have changed.
        abstract textEditor: TextEditor with get, set
        /// The new value for the [text editor's selections](#TextEditor.selections).
        abstract selections: ResizeArray<Selection> with get, set
        /// The [change kind](#TextEditorSelectionChangeKind) which has triggered this
        /// event. Can be `undefined`.
        abstract kind: TextEditorSelectionChangeKind option with get, set

    /// Represents an event describing the change in a [text editor's options](#TextEditor.options).
    type [<AllowNullLiteral>] TextEditorOptionsChangeEvent =
        /// The [text editor](#TextEditor) for which the options have changed.
        abstract textEditor: TextEditor with get, set
        /// The new value for the [text editor's options](#TextEditor.options).
        abstract options: TextEditorOptions with get, set

    /// Represents an event describing the change of a [text editor's view column](#TextEditor.viewColumn).
    type [<AllowNullLiteral>] TextEditorViewColumnChangeEvent =
        /// The [text editor](#TextEditor) for which the options have changed.
        abstract textEditor: TextEditor with get, set
        /// The new value for the [text editor's view column](#TextEditor.viewColumn).
        abstract viewColumn: ViewColumn with get, set

    type [<RequireQualifiedAccess>] TextEditorCursorStyle =
        | Line = 1
        | Block = 2
        | Underline = 3
        | LineThin = 4
        | BlockOutline = 5
        | UnderlineThin = 6

    type [<RequireQualifiedAccess>] TextEditorLineNumbersStyle =
        | Off = 0
        | On = 1
        | Relative = 2

    /// Represents a [text editor](#TextEditor)'s [options](#TextEditor.options).
    type [<AllowNullLiteral>] TextEditorOptions =
        /// The size in spaces a tab takes. This is used for two purposes:
        ///   - the rendering width of a tab character;
        ///   - the number of spaces to insert when [insertSpaces](#TextEditorOptions.insertSpaces) is true.
        /// 
        /// When getting a text editor's options, this property will always be a number (resolved).
        /// When setting a text editor's options, this property is optional and it can be a number or `"auto"`.
        abstract tabSize: U2<float, string> option with get, set
        /// When pressing Tab insert [n](#TextEditorOptions.tabSize) spaces.
        /// When getting a text editor's options, this property will always be a boolean (resolved).
        /// When setting a text editor's options, this property is optional and it can be a boolean or `"auto"`.
        abstract insertSpaces: U2<bool, string> option with get, set
        /// The rendering style of the cursor in this editor.
        /// When getting a text editor's options, this property will always be present.
        /// When setting a text editor's options, this property is optional.
        abstract cursorStyle: TextEditorCursorStyle option with get, set
        /// Render relative line numbers w.r.t. the current line number.
        /// When getting a text editor's options, this property will always be present.
        /// When setting a text editor's options, this property is optional.
        abstract lineNumbers: TextEditorLineNumbersStyle option with get, set

    /// Represents a handle to a set of decorations
    /// sharing the same [styling options](#DecorationRenderOptions) in a [text editor](#TextEditor).
    /// 
    /// To get an instance of a `TextEditorDecorationType` use
    /// [createTextEditorDecorationType](#window.createTextEditorDecorationType).
    type [<AllowNullLiteral>] TextEditorDecorationType =
        /// Internal representation of the handle.
        abstract key: string
        /// Remove this decoration type and all decorations on all text editors using it.
        abstract dispose: unit -> unit

    type [<RequireQualifiedAccess>] TextEditorRevealType =
        | Default = 0
        | InCenter = 1
        | InCenterIfOutsideViewport = 2
        | AtTop = 3

    type [<RequireQualifiedAccess>] OverviewRulerLane =
        | Left = 1
        | Center = 2
        | Right = 4
        | Full = 7

    type [<RequireQualifiedAccess>] DecorationRangeBehavior =
        | OpenOpen = 0
        | ClosedClosed = 1
        | OpenClosed = 2
        | ClosedOpen = 3

    /// Represents options to configure the behavior of showing a [document](#TextDocument) in an [editor](#TextEditor).
    type [<AllowNullLiteral>] TextDocumentShowOptions =
        /// An optional view column in which the [editor](#TextEditor) should be shown.
        /// The default is the [one](#ViewColumn.One), other values are adjusted to
        /// be `Min(column, columnCount + 1)`, the [active](#ViewColumn.Active)-column is
        /// not adjusted.
        abstract viewColumn: ViewColumn option with get, set
        /// An optional flag that when `true` will stop the [editor](#TextEditor) from taking focus.
        abstract preserveFocus: bool option with get, set
        /// An optional flag that controls if an [editor](#TextEditor)-tab will be replaced
        /// with the next editor or if it will be kept.
        abstract preview: bool option with get, set
        /// An optional selection to apply for the document in the [editor](#TextEditor).
        abstract selection: Range option with get, set

    /// A reference to one of the workbench colors as defined in https://code.visualstudio.com/docs/getstarted/theme-color-reference.
    /// Using a theme color is preferred over a custom color as it gives theme authors and users the possibility to change the color.
    type [<AllowNullLiteral>] ThemeColor =
        interface end

    /// A reference to one of the workbench colors as defined in https://code.visualstudio.com/docs/getstarted/theme-color-reference.
    /// Using a theme color is preferred over a custom color as it gives theme authors and users the possibility to change the color.
    type [<AllowNullLiteral>] ThemeColorStatic =
        /// <summary>Creates a reference to a theme color.</summary>
        /// <param name="id">of the color. The available colors are listed in https://code.visualstudio.com/docs/getstarted/theme-color-reference.</param>
        [<Emit "new $0($1...)">] abstract Create: id: string -> ThemeColor

    /// Represents theme specific rendering styles for a [text editor decoration](#TextEditorDecorationType).
    type [<AllowNullLiteral>] ThemableDecorationRenderOptions =
        /// Background color of the decoration. Use rgba() and define transparent background colors to play well with other decorations.
        /// Alternatively a color from the color registry can be [referenced](#ThemeColor).
        abstract backgroundColor: U2<string, ThemeColor> option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        abstract outline: string option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        /// Better use 'outline' for setting one or more of the individual outline properties.
        abstract outlineColor: U2<string, ThemeColor> option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        /// Better use 'outline' for setting one or more of the individual outline properties.
        abstract outlineStyle: string option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        /// Better use 'outline' for setting one or more of the individual outline properties.
        abstract outlineWidth: string option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        abstract border: string option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        /// Better use 'border' for setting one or more of the individual border properties.
        abstract borderColor: U2<string, ThemeColor> option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        /// Better use 'border' for setting one or more of the individual border properties.
        abstract borderRadius: string option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        /// Better use 'border' for setting one or more of the individual border properties.
        abstract borderSpacing: string option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        /// Better use 'border' for setting one or more of the individual border properties.
        abstract borderStyle: string option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        /// Better use 'border' for setting one or more of the individual border properties.
        abstract borderWidth: string option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        abstract textDecoration: string option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        abstract cursor: string option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        abstract color: U2<string, ThemeColor> option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        abstract letterSpacing: string option with get, set
        /// An **absolute path** or an URI to an image to be rendered in the gutter.
        abstract gutterIconPath: U2<string, Uri> option with get, set
        /// Specifies the size of the gutter icon.
        /// Available values are 'auto', 'contain', 'cover' and any percentage value.
        /// For further information: https://msdn.microsoft.com/en-us/library/jj127316(v=vs.85).aspx
        abstract gutterIconSize: string option with get, set
        /// The color of the decoration in the overview ruler. Use rgba() and define transparent colors to play well with other decorations.
        abstract overviewRulerColor: U2<string, ThemeColor> option with get, set
        /// Defines the rendering options of the attachment that is inserted before the decorated text
        abstract before: ThemableDecorationAttachmentRenderOptions option with get, set
        /// Defines the rendering options of the attachment that is inserted after the decorated text
        abstract after: ThemableDecorationAttachmentRenderOptions option with get, set

    type [<AllowNullLiteral>] ThemableDecorationAttachmentRenderOptions =
        /// Defines a text content that is shown in the attachment. Either an icon or a text can be shown, but not both.
        abstract contentText: string option with get, set
        /// An **absolute path** or an URI to an image to be rendered in the attachment. Either an icon
        /// or a text can be shown, but not both.
        abstract contentIconPath: U2<string, Uri> option with get, set
        /// CSS styling property that will be applied to the decoration attachment.
        abstract border: string option with get, set
        /// CSS styling property that will be applied to text enclosed by a decoration.
        abstract borderColor: U2<string, ThemeColor> option with get, set
        /// CSS styling property that will be applied to the decoration attachment.
        abstract textDecoration: string option with get, set
        /// CSS styling property that will be applied to the decoration attachment.
        abstract color: U2<string, ThemeColor> option with get, set
        /// CSS styling property that will be applied to the decoration attachment.
        abstract backgroundColor: U2<string, ThemeColor> option with get, set
        /// CSS styling property that will be applied to the decoration attachment.
        abstract margin: string option with get, set
        /// CSS styling property that will be applied to the decoration attachment.
        abstract width: string option with get, set
        /// CSS styling property that will be applied to the decoration attachment.
        abstract height: string option with get, set

    /// Represents rendering styles for a [text editor decoration](#TextEditorDecorationType).
    type [<AllowNullLiteral>] DecorationRenderOptions =
        inherit ThemableDecorationRenderOptions
        /// Should the decoration be rendered also on the whitespace after the line text.
        /// Defaults to `false`.
        abstract isWholeLine: bool option with get, set
        /// Customize the growing behavior of the decoration when edits occur at the edges of the decoration's range.
        /// Defaults to `DecorationRangeBehavior.OpenOpen`.
        abstract rangeBehavior: DecorationRangeBehavior option with get, set
        /// The position in the overview ruler where the decoration should be rendered.
        abstract overviewRulerLane: OverviewRulerLane option with get, set
        /// Overwrite options for light themes.
        abstract light: ThemableDecorationRenderOptions option with get, set
        /// Overwrite options for dark themes.
        abstract dark: ThemableDecorationRenderOptions option with get, set

    /// Represents options for a specific decoration in a [decoration set](#TextEditorDecorationType).
    type [<AllowNullLiteral>] DecorationOptions =
        /// Range to which this decoration is applied. The range must not be empty.
        abstract range: Range with get, set
        /// A message that should be rendered when hovering over the decoration.
        abstract hoverMessage: U2<MarkedString, ResizeArray<MarkedString>> option with get, set
        /// Render options applied to the current decoration. For performance reasons, keep the
        /// number of decoration specific options small, and use decoration types whereever possible.
        abstract renderOptions: DecorationInstanceRenderOptions option with get, set

    type [<AllowNullLiteral>] ThemableDecorationInstanceRenderOptions =
        /// Defines the rendering options of the attachment that is inserted before the decorated text
        abstract before: ThemableDecorationAttachmentRenderOptions option with get, set
        /// Defines the rendering options of the attachment that is inserted after the decorated text
        abstract after: ThemableDecorationAttachmentRenderOptions option with get, set

    type [<AllowNullLiteral>] DecorationInstanceRenderOptions =
        inherit ThemableDecorationInstanceRenderOptions
        /// Overwrite options for light themes.
        abstract light: ThemableDecorationInstanceRenderOptions option with get, set
        /// Overwrite options for dark themes.
        abstract dark: ThemableDecorationInstanceRenderOptions option with get, set

    /// Represents an editor that is attached to a [document](#TextDocument).
    type [<AllowNullLiteral>] TextEditor =
        /// The document associated with this text editor. The document will be the same for the entire lifetime of this text editor.
        abstract document: TextDocument with get, set
        /// The primary selection on this text editor. Shorthand for `TextEditor.selections[0]`.
        abstract selection: Selection with get, set
        /// The selections in this text editor. The primary selection is always at index 0.
        abstract selections: ResizeArray<Selection> with get, set
        /// Text editor options.
        abstract options: TextEditorOptions with get, set
        /// The column in which this editor shows. Will be `undefined` in case this
        /// isn't one of the three main editors, e.g an embedded editor.
        abstract viewColumn: ViewColumn option with get, set
        /// <summary>Perform an edit on the document associated with this text editor.
        /// 
        /// The given callback-function is invoked with an [edit-builder](#TextEditorEdit) which must
        /// be used to make edits. Note that the edit-builder is only valid while the
        /// callback executes.</summary>
        /// <param name="callback">A function which can create edits using an [edit-builder](#TextEditorEdit).</param>
        /// <param name="options">The undo/redo behavior around this edit. By default, undo stops will be created before and after this edit.</param>
        abstract edit: callback: (TextEditorEdit -> unit) * ?options: TextEditorEditOptions -> Thenable<bool>
        /// <summary>Insert a [snippet](#SnippetString) and put the editor into snippet mode. "Snippet mode"
        /// means the editor adds placeholders and additionals cursors so that the user can complete
        /// or accept the snippet.</summary>
        /// <param name="snippet">The snippet to insert in this edit.</param>
        /// <param name="location">Position or range at which to insert the snippet, defaults to the current editor selection or selections.</param>
        /// <param name="options">The undo/redo behavior around this edit. By default, undo stops will be created before and after this edit.</param>
        abstract insertSnippet: snippet: SnippetString * ?location: U4<Position, Range, ResizeArray<Position>, ResizeArray<Range>> * ?options: TextEditorInsertSnippetOptions -> Thenable<bool>
        /// <summary>Adds a set of decorations to the text editor. If a set of decorations already exists with
        /// the given [decoration type](#TextEditorDecorationType), they will be replaced.</summary>
        /// <param name="decorationType">A decoration type.</param>
        /// <param name="rangesOrOptions">Either [ranges](#Range) or more detailed [options](#DecorationOptions).</param>
        abstract setDecorations: decorationType: TextEditorDecorationType * rangesOrOptions: U2<ResizeArray<Range>, ResizeArray<DecorationOptions>> -> unit
        /// <summary>Scroll as indicated by `revealType` in order to reveal the given range.</summary>
        /// <param name="range">A range.</param>
        /// <param name="revealType">The scrolling strategy for revealing `range`.</param>
        abstract revealRange: range: Range * ?revealType: TextEditorRevealType -> unit
        /// <summary>~~Show the text editor.~~</summary>
        /// <param name="column">The [column](#ViewColumn) in which to show this editor.
        /// instead. This method shows unexpected behavior and will be removed in the next major update.</param>
        abstract show: ?column: ViewColumn -> unit
        /// ~~Hide the text editor.~~
        abstract hide: unit -> unit

    type [<AllowNullLiteral>] TextEditorEditOptions =
        abstract undoStopBefore: bool with get, set
        abstract undoStopAfter: bool with get, set

    type [<AllowNullLiteral>] TextEditorInsertSnippetOptions =
        abstract undoStopBefore: bool with get, set
        abstract undoStopAfter: bool with get, set

    type [<RequireQualifiedAccess>] EndOfLine =
        | LF = 1
        | CRLF = 2

    /// A complex edit that will be applied in one transaction on a TextEditor.
    /// This holds a description of the edits and if the edits are valid (i.e. no overlapping regions, document was not changed in the meantime, etc.)
    /// they can be applied on a [document](#TextDocument) associated with a [text editor](#TextEditor).
    type [<AllowNullLiteral>] TextEditorEdit =
        /// <summary>Replace a certain text region with a new value.
        /// You can use \r\n or \n in `value` and they will be normalized to the current [document](#TextDocument).</summary>
        /// <param name="location">The range this operation should remove.</param>
        /// <param name="value">The new text this operation should insert after removing `location`.</param>
        abstract replace: location: U3<Position, Range, Selection> * value: string -> unit
        /// <summary>Insert text at a location.
        /// You can use \r\n or \n in `value` and they will be normalized to the current [document](#TextDocument).
        /// Although the equivalent text edit can be made with [replace](#TextEditorEdit.replace), `insert` will produce a different resulting selection (it will get moved).</summary>
        /// <param name="location">The position where the new text should be inserted.</param>
        /// <param name="value">The new text this operation should insert.</param>
        abstract insert: location: Position * value: string -> unit
        /// <summary>Delete a certain text region.</summary>
        /// <param name="location">The range this operation should remove.</param>
        abstract delete: location: U2<Range, Selection> -> unit
        /// <summary>Set the end of line sequence.</summary>
        /// <param name="endOfLine">The new end of line for the [document](#TextDocument).</param>
        abstract setEndOfLine: endOfLine: EndOfLine -> unit

    /// A universal resource identifier representing either a file on disk
    /// or another resource, like untitled resources.
    type [<AllowNullLiteral>] Uri =
        /// Scheme is the `http` part of `http://www.msft.com/some/path?query#fragment`.
        /// The part before the first colon.
        abstract scheme: string
        /// Authority is the `www.msft.com` part of `http://www.msft.com/some/path?query#fragment`.
        /// The part between the first double slashes and the next slash.
        abstract authority: string
        /// Path is the `/some/path` part of `http://www.msft.com/some/path?query#fragment`.
        abstract path: string
        /// Query is the `query` part of `http://www.msft.com/some/path?query#fragment`.
        abstract query: string
        /// Fragment is the `fragment` part of `http://www.msft.com/some/path?query#fragment`.
        abstract fragment: string
        /// The string representing the corresponding file system path of this Uri.
        /// 
        /// Will handle UNC paths and normalize windows drive letters to lower-case. Also
        /// uses the platform specific path separator. Will *not* validate the path for
        /// invalid characters and semantics. Will *not* look at the scheme of this Uri.
        abstract fsPath: string
        /// <summary>Derive a new Uri from this Uri.
        /// 
        /// ```ts
        /// let file = Uri.parse('before:some/file/path');
        /// let other = file.with({ scheme: 'after' });
        /// assert.ok(other.toString() === 'after:some/file/path');
        /// ```</summary>
        /// <param name="change">An object that describes a change to this Uri. To unset components use `null` or
        /// the empty string.</param>
        abstract ``with``: change: UriWithChange -> Uri
        /// <summary>Returns a string representation of this Uri. The representation and normalization
        /// of a URI depends on the scheme. The resulting string can be safely used with
        /// [Uri.parse](#Uri.parse).</summary>
        /// <param name="skipEncoding">Do not percentage-encode the result, defaults to `false`. Note that
        /// the `#` and `?` characters occuring in the path will always be encoded.</param>
        abstract toString: ?skipEncoding: bool -> string
        /// Returns a JSON representation of this Uri.
        abstract toJSON: unit -> obj option

    type [<AllowNullLiteral>] UriWithChange =
        abstract scheme: string option with get, set
        abstract authority: string option with get, set
        abstract path: string option with get, set
        abstract query: string option with get, set
        abstract fragment: string option with get, set

    /// A universal resource identifier representing either a file on disk
    /// or another resource, like untitled resources.
    type [<AllowNullLiteral>] UriStatic =
        /// <summary>Create an URI from a file system path. The [scheme](#Uri.scheme)
        /// will be `file`.</summary>
        /// <param name="path">A file system or UNC path.</param>
        abstract file: path: string -> Uri
        /// <summary>Create an URI from a string. Will throw if the given value is not
        /// valid.</summary>
        /// <param name="value">The string value of an Uri.</param>
        abstract parse: value: string -> Uri
        /// Use the `file` and `parse` factory functions to create new `Uri` objects.
        [<Emit "new $0($1...)">] abstract Create: scheme: string * authority: string * path: string * query: string * fragment: string -> Uri

    /// A cancellation token is passed to an asynchronous or long running
    /// operation to request cancellation, like cancelling a request
    /// for completion items because the user continued to type.
    /// 
    /// To get an instance of a `CancellationToken` use a
    /// [CancellationTokenSource](#CancellationTokenSource).
    type [<AllowNullLiteral>] CancellationToken =
        /// Is `true` when the token has been cancelled, `false` otherwise.
        abstract isCancellationRequested: bool with get, set
        /// An [event](#Event) which fires upon cancellation.
        abstract onCancellationRequested: Event<obj option> with get, set

    /// A cancellation source creates and controls a [cancellation token](#CancellationToken).
    type [<AllowNullLiteral>] CancellationTokenSource =
        /// The cancellation token of this source.
        abstract token: CancellationToken with get, set
        /// Signal cancellation on the token.
        abstract cancel: unit -> unit
        /// Dispose object and free resources. Will call [cancel](#CancellationTokenSource.cancel).
        abstract dispose: unit -> unit

    /// A cancellation source creates and controls a [cancellation token](#CancellationToken).
    type [<AllowNullLiteral>] CancellationTokenSourceStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> CancellationTokenSource

    /// Represents a type which can release resources, such
    /// as event listening or a timer.
    type [<AllowNullLiteral>] Disposable =
        /// Dispose this object.
        abstract dispose: unit -> obj option

    /// Represents a type which can release resources, such
    /// as event listening or a timer.
    type [<AllowNullLiteral>] DisposableStatic =
        /// <summary>Combine many disposable-likes into one. Use this method
        /// when having objects with a dispose function which are not
        /// instances of Disposable.</summary>
        /// <param name="disposableLikes">Objects that have at least a `dispose`-function member.</param>
        abstract from: [<ParamArray>] disposableLikes: ResizeArray<obj> -> Disposable
        /// <summary>Creates a new Disposable calling the provided function
        /// on dispose.</summary>
        /// <param name="callOnDispose">Function that disposes something.</param>
        [<Emit "new $0($1...)">] abstract Create: callOnDispose: Function -> Disposable

    /// Represents a typed event.
    /// 
    /// A function that represents an event to which you subscribe by calling it with
    /// a listener function as argument.
    type [<AllowNullLiteral>] Event<'T> =
        /// <summary>A function that represents an event to which you subscribe by calling it with
        /// a listener function as argument.</summary>
        /// <param name="listener">The listener function will be called when the event happens.</param>
        /// <param name="thisArgs">The `this`-argument which will be used when calling the event listener.</param>
        /// <param name="disposables">An array to which a [disposable](#Disposable) will be added.</param>
        [<Emit "$0($1...)">] abstract Invoke: listener: ('T -> obj option) * ?thisArgs: obj option * ?disposables: ResizeArray<Disposable> -> Disposable

    /// An event emitter can be used to create and manage an [event](#Event) for others
    /// to subscribe to. One emitter always owns one event.
    /// 
    /// Use this class if you want to provide event from within your extension, for instance
    /// inside a [TextDocumentContentProvider](#TextDocumentContentProvider) or when providing
    /// API to other extensions.
    type [<AllowNullLiteral>] EventEmitter<'T> =
        /// The event listeners can subscribe to.
        abstract ``event``: Event<'T> with get, set
        /// <summary>Notify all subscribers of the [event](EventEmitter#event). Failure
        /// of one or more listener will not fail this function call.</summary>
        /// <param name="data">The event object.</param>
        abstract fire: ?data: 'T -> unit
        /// Dispose this object and free resources.
        abstract dispose: unit -> unit

    /// An event emitter can be used to create and manage an [event](#Event) for others
    /// to subscribe to. One emitter always owns one event.
    /// 
    /// Use this class if you want to provide event from within your extension, for instance
    /// inside a [TextDocumentContentProvider](#TextDocumentContentProvider) or when providing
    /// API to other extensions.
    type [<AllowNullLiteral>] EventEmitterStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> EventEmitter<'T>

    /// A file system watcher notifies about changes to files and folders
    /// on disk.
    /// 
    /// To get an instance of a `FileSystemWatcher` use
    /// [createFileSystemWatcher](#workspace.createFileSystemWatcher).
    type [<AllowNullLiteral>] FileSystemWatcher =
        inherit Disposable
        /// true if this file system watcher has been created such that
        /// it ignores creation file system events.
        abstract ignoreCreateEvents: bool with get, set
        /// true if this file system watcher has been created such that
        /// it ignores change file system events.
        abstract ignoreChangeEvents: bool with get, set
        /// true if this file system watcher has been created such that
        /// it ignores delete file system events.
        abstract ignoreDeleteEvents: bool with get, set
        /// An event which fires on file/folder creation.
        abstract onDidCreate: Event<Uri> with get, set
        /// An event which fires on file/folder change.
        abstract onDidChange: Event<Uri> with get, set
        /// An event which fires on file/folder deletion.
        abstract onDidDelete: Event<Uri> with get, set

    /// A text document content provider allows to add readonly documents
    /// to the editor, such as source from a dll or generated html from md.
    /// 
    /// Content providers are [registered](#workspace.registerTextDocumentContentProvider)
    /// for a [uri-scheme](#Uri.scheme). When a uri with that scheme is to
    /// be [loaded](#workspace.openTextDocument) the content provider is
    /// asked.
    type [<AllowNullLiteral>] TextDocumentContentProvider =
        /// An event to signal a resource has changed.
        abstract onDidChange: Event<Uri> option with get, set
        /// <summary>Provide textual content for a given uri.
        /// 
        /// The editor will use the returned string-content to create a readonly
        /// [document](#TextDocument). Resources allocated should be released when
        /// the corresponding document has been [closed](#workspace.onDidCloseTextDocument).</summary>
        /// <param name="uri">An uri which scheme matches the scheme this provider was [registered](#workspace.registerTextDocumentContentProvider) for.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideTextDocumentContent: uri: Uri * token: CancellationToken -> ProviderResult<string>

    /// Represents an item that can be selected from
    /// a list of items.
    type [<AllowNullLiteral>] QuickPickItem =
        /// A human readable string which is rendered prominent.
        abstract label: string with get, set
        /// A human readable string which is rendered less prominent.
        abstract description: string with get, set
        /// A human readable string which is rendered less prominent.
        abstract detail: string option with get, set

    /// Options to configure the behavior of the quick pick UI.
    type [<AllowNullLiteral>] QuickPickOptions =
        /// An optional flag to include the description when filtering the picks.
        abstract matchOnDescription: bool option with get, set
        /// An optional flag to include the detail when filtering the picks.
        abstract matchOnDetail: bool option with get, set
        /// An optional string to show as place holder in the input box to guide the user what to pick on.
        abstract placeHolder: string option with get, set
        /// Set to `true` to keep the picker open when focus moves to another part of the editor or to another window.
        abstract ignoreFocusOut: bool option with get, set
        /// An optional function that is invoked whenever an item is selected.
        abstract onDidSelectItem: item: U2<QuickPickItem, string> -> obj option

    /// Options to configure the behaviour of the [workspace folder](#WorkspaceFolder) pick UI.
    type [<AllowNullLiteral>] WorkspaceFolderPickOptions =
        /// An optional string to show as place holder in the input box to guide the user what to pick on.
        abstract placeHolder: string option with get, set
        /// Set to `true` to keep the picker open when focus moves to another part of the editor or to another window.
        abstract ignoreFocusOut: bool option with get, set

    /// Options to configure the behaviour of a file open dialog.
    /// 
    /// * Note 1: A dialog can select files, folders, or both. This is not true for Windows
    /// which enforces to open either files or folder, but *not both*.
    /// * Note 2: Explictly setting `canSelectFiles` and `canSelectFolders` to `false` is futile
    /// and the editor then silently adjusts the options to select files.
    type [<AllowNullLiteral>] OpenDialogOptions =
        /// The resource the dialog shows when opened.
        abstract defaultUri: Uri option with get, set
        /// A human-readable string for the open button.
        abstract openLabel: string option with get, set
        /// Allow to select files, defaults to `true`.
        abstract canSelectFiles: bool option with get, set
        /// Allow to select folders, defaults to `false`.
        abstract canSelectFolders: bool option with get, set
        /// Allow to select many files or folders.
        abstract canSelectMany: bool option with get, set
        /// A set of file filters that are used by the dialog. Each entry is a human readable label,
        /// like "TypeScript", and an array of extensions, e.g.
        /// ```ts
        /// {
        ///  	'Images': ['png', 'jpg']
        ///  	'TypeScript': ['ts', 'tsx']
        /// }
        /// ```
        abstract filters: obj option with get, set

    /// Options to configure the behaviour of a file save dialog.
    type [<AllowNullLiteral>] SaveDialogOptions =
        /// The resource the dialog shows when opened.
        abstract defaultUri: Uri option with get, set
        /// A human-readable string for the save button.
        abstract saveLabel: string option with get, set
        /// A set of file filters that are used by the dialog. Each entry is a human readable label,
        /// like "TypeScript", and an array of extensions, e.g.
        /// ```ts
        /// {
        ///  	'Images': ['png', 'jpg']
        ///  	'TypeScript': ['ts', 'tsx']
        /// }
        /// ```
        abstract filters: obj option with get, set

    /// Represents an action that is shown with an information, warning, or
    /// error message.
    type [<AllowNullLiteral>] MessageItem =
        /// A short title like 'Retry', 'Open Log' etc.
        abstract title: string with get, set
        /// Indicates that this item replaces the default
        /// 'Close' action.
        abstract isCloseAffordance: bool option with get, set

    /// Options to configure the behavior of the message.
    type [<AllowNullLiteral>] MessageOptions =
        /// Indicates that this message should be modal.
        abstract modal: bool option with get, set

    /// Options to configure the behavior of the input box UI.
    type [<AllowNullLiteral>] InputBoxOptions =
        /// The value to prefill in the input box.
        abstract value: string option with get, set
        /// Selection of the prefilled [`value`](#InputBoxOptions.value). Defined as tuple of two number where the
        /// first is the inclusive start index and the second the exclusive end index. When `undefined` the whole
        /// word will be selected, when empty (start equals end) only the cursor will be set,
        /// otherwise the defined range will be selected.
        abstract valueSelection: float * float option with get, set
        /// The text to display underneath the input box.
        abstract prompt: string option with get, set
        /// An optional string to show as place holder in the input box to guide the user what to type.
        abstract placeHolder: string option with get, set
        /// Set to `true` to show a password prompt that will not show the typed value.
        abstract password: bool option with get, set
        /// Set to `true` to keep the input box open when focus moves to another part of the editor or to another window.
        abstract ignoreFocusOut: bool option with get, set
        /// <summary>An optional function that will be called to validate input and to give a hint
        /// to the user.</summary>
        /// <param name="value">The current value of the input box.</param>
        abstract validateInput: value: string -> U2<string, Thenable<string option>> option

    /// A relative pattern is a helper to construct glob patterns that are matched
    /// relatively to a base path. The base path can either be an absolute file path
    /// or a [workspace folder](#WorkspaceFolder).
    type [<AllowNullLiteral>] RelativePattern =
        /// A base file path to which this pattern will be matched against relatively.
        abstract ``base``: string with get, set
        /// A file glob pattern like `*.{ts,js}` that will be matched on file paths
        /// relative to the base path.
        /// 
        /// Example: Given a base of `/home/work/folder` and a file path of `/home/work/folder/index.js`,
        /// the file glob pattern will match on `index.js`.
        abstract pattern: string with get, set

    /// A relative pattern is a helper to construct glob patterns that are matched
    /// relatively to a base path. The base path can either be an absolute file path
    /// or a [workspace folder](#WorkspaceFolder).
    type [<AllowNullLiteral>] RelativePatternStatic =
        /// <summary>Creates a new relative pattern object with a base path and pattern to match. This pattern
        /// will be matched on file paths relative to the base path.</summary>
        /// <param name="base">A base file path to which this pattern will be matched against relatively.</param>
        /// <param name="pattern">A file glob pattern like `*.{ts,js}` that will be matched on file paths
        /// relative to the base path.</param>
        [<Emit "new $0($1...)">] abstract Create: ``base``: U2<WorkspaceFolder, string> * pattern: string -> RelativePattern

    type GlobPattern =
        U2<string, RelativePattern>

    [<RequireQualifiedAccess; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module GlobPattern =
        let ofString v: GlobPattern = v |> U2.Case1
        let isString (v: GlobPattern) = match v with U2.Case1 _ -> true | _ -> false
        let asString (v: GlobPattern) = match v with U2.Case1 o -> Some o | _ -> None
        let ofRelativePattern v: GlobPattern = v |> U2.Case2
        let isRelativePattern (v: GlobPattern) = match v with U2.Case2 _ -> true | _ -> false
        let asRelativePattern (v: GlobPattern) = match v with U2.Case2 o -> Some o | _ -> None

    /// A document filter denotes a document by different properties like
    /// the [language](#TextDocument.languageId), the [scheme](#Uri.scheme) of
    /// its resource, or a glob-pattern that is applied to the [path](#TextDocument.fileName).
    type [<AllowNullLiteral>] DocumentFilter =
        /// A language id, like `typescript`.
        abstract language: string option with get, set
        /// A Uri [scheme](#Uri.scheme), like `file` or `untitled`.
        abstract scheme: string option with get, set
        /// A [glob pattern](#GlobPattern) that is matched on the absolute path of the document. Use a [relative pattern](#RelativePattern)
        /// to filter documents to a [workspace folder](#WorkspaceFolder).
        abstract pattern: GlobPattern option with get, set

    type DocumentSelector =
        U3<string, DocumentFilter, ResizeArray<U2<string, DocumentFilter>>>

    [<RequireQualifiedAccess; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module DocumentSelector =
        let ofString v: DocumentSelector = v |> U3.Case1
        let isString (v: DocumentSelector) = match v with U3.Case1 _ -> true | _ -> false
        let asString (v: DocumentSelector) = match v with U3.Case1 o -> Some o | _ -> None
        let ofDocumentFilter v: DocumentSelector = v |> U3.Case2
        let isDocumentFilter (v: DocumentSelector) = match v with U3.Case2 _ -> true | _ -> false
        let asDocumentFilter (v: DocumentSelector) = match v with U3.Case2 o -> Some o | _ -> None
        let ofCase3 v: DocumentSelector = v |> U3.Case3
        let isCase3 (v: DocumentSelector) = match v with U3.Case3 _ -> true | _ -> false
        let asCase3 (v: DocumentSelector) = match v with U3.Case3 o -> Some o | _ -> None

    type ProviderResult<'T> =
        U2<'T, Thenable<'T option>> option

    [<RequireQualifiedAccess; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module ProviderResult =
        let ofTOption v: ProviderResult<'T> = v |> Option.map U2.Case1
        let ofT v: ProviderResult<'T> = v |> U2.Case1 |> Some
        let isT (v: ProviderResult<'T>) = match v with None -> false | Some o -> match o with U2.Case1 _ -> true | _ -> false
        let asT (v: ProviderResult<'T>) = match v with None -> None | Some o -> match o with U2.Case1 o -> Some o | _ -> None
        let ofThenableOption v: ProviderResult<'T> = v |> Option.map U2.Case2
        let ofThenable v: ProviderResult<'T> = v |> U2.Case2 |> Some
        let isThenable (v: ProviderResult<'T>) = match v with None -> false | Some o -> match o with U2.Case2 _ -> true | _ -> false
        let asThenable (v: ProviderResult<'T>) = match v with None -> None | Some o -> match o with U2.Case2 o -> Some o | _ -> None

    /// Contains additional diagnostic information about the context in which
    /// a [code action](#CodeActionProvider.provideCodeActions) is run.
    type [<AllowNullLiteral>] CodeActionContext =
        /// An array of diagnostics.
        abstract diagnostics: ResizeArray<Diagnostic>

    /// The code action interface defines the contract between extensions and
    /// the [light bulb](https://code.visualstudio.com/docs/editor/editingevolved#_code-action) feature.
    /// 
    /// A code action can be any command that is [known](#commands.getCommands) to the system.
    type [<AllowNullLiteral>] CodeActionProvider =
        /// <summary>Provide commands for the given document and range.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="range">The range for which the command was invoked.</param>
        /// <param name="context">Context carrying additional information.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideCodeActions: document: TextDocument * range: Range * context: CodeActionContext * token: CancellationToken -> ProviderResult<ResizeArray<Command>>

    /// A code lens represents a [command](#Command) that should be shown along with
    /// source text, like the number of references, a way to run tests, etc.
    /// 
    /// A code lens is _unresolved_ when no command is associated to it. For performance
    /// reasons the creation of a code lens and resolving should be done to two stages.
    type [<AllowNullLiteral>] CodeLens =
        /// The range in which this code lens is valid. Should only span a single line.
        abstract range: Range with get, set
        /// The command this code lens represents.
        abstract command: Command option with get, set
        /// `true` when there is a command associated.
        abstract isResolved: bool

    /// A code lens represents a [command](#Command) that should be shown along with
    /// source text, like the number of references, a way to run tests, etc.
    /// 
    /// A code lens is _unresolved_ when no command is associated to it. For performance
    /// reasons the creation of a code lens and resolving should be done to two stages.
    type [<AllowNullLiteral>] CodeLensStatic =
        /// <summary>Creates a new code lens object.</summary>
        /// <param name="range">The range to which this code lens applies.</param>
        /// <param name="command">The command associated to this code lens.</param>
        [<Emit "new $0($1...)">] abstract Create: range: Range * ?command: Command -> CodeLens

    /// A code lens provider adds [commands](#Command) to source text. The commands will be shown
    /// as dedicated horizontal lines in between the source text.
    type [<AllowNullLiteral>] CodeLensProvider =
        /// An optional event to signal that the code lenses from this provider have changed.
        abstract onDidChangeCodeLenses: Event<unit> option with get, set
        /// <summary>Compute a list of [lenses](#CodeLens). This call should return as fast as possible and if
        /// computing the commands is expensive implementors should only return code lens objects with the
        /// range set and implement [resolve](#CodeLensProvider.resolveCodeLens).</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideCodeLenses: document: TextDocument * token: CancellationToken -> ProviderResult<ResizeArray<CodeLens>>
        /// <summary>This function will be called for each visible code lens, usually when scrolling and after
        /// calls to [compute](#CodeLensProvider.provideCodeLenses)-lenses.</summary>
        /// <param name="codeLens">code lens that must be resolved.</param>
        /// <param name="token">A cancellation token.</param>
        abstract resolveCodeLens: codeLens: CodeLens * token: CancellationToken -> ProviderResult<CodeLens>

    type Definition =
        U2<Location, ResizeArray<Location>>

    [<RequireQualifiedAccess; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Definition =
        let ofLocation v: Definition = v |> U2.Case1
        let isLocation (v: Definition) = match v with U2.Case1 _ -> true | _ -> false
        let asLocation (v: Definition) = match v with U2.Case1 o -> Some o | _ -> None
        let ofLocationArray v: Definition = v |> U2.Case2
        let isLocationArray (v: Definition) = match v with U2.Case2 _ -> true | _ -> false
        let asLocationArray (v: Definition) = match v with U2.Case2 o -> Some o | _ -> None

    /// The definition provider interface defines the contract between extensions and
    /// the [go to definition](https://code.visualstudio.com/docs/editor/editingevolved#_go-to-definition)
    /// and peek definition features.
    type [<AllowNullLiteral>] DefinitionProvider =
        /// <summary>Provide the definition of the symbol at the given position and document.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="position">The position at which the command was invoked.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideDefinition: document: TextDocument * position: Position * token: CancellationToken -> ProviderResult<Definition>

    /// The implemenetation provider interface defines the contract between extensions and
    /// the go to implementation feature.
    type [<AllowNullLiteral>] ImplementationProvider =
        /// <summary>Provide the implementations of the symbol at the given position and document.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="position">The position at which the command was invoked.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideImplementation: document: TextDocument * position: Position * token: CancellationToken -> ProviderResult<Definition>

    /// The type definition provider defines the contract between extensions and
    /// the go to type definition feature.
    type [<AllowNullLiteral>] TypeDefinitionProvider =
        /// <summary>Provide the type definition of the symbol at the given position and document.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="position">The position at which the command was invoked.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideTypeDefinition: document: TextDocument * position: Position * token: CancellationToken -> ProviderResult<Definition>

    /// The MarkdownString represents human readable text that supports formatting via the
    /// markdown syntax. Standard markdown is supported, also tables, but no embedded html.
    type [<AllowNullLiteral>] MarkdownString =
        /// The markdown string.
        abstract value: string with get, set
        /// Indicates that this markdown string is from a trusted source. Only *trusted*
        /// markdown supports links that execute commands, e.g. `[Run it](command:myCommandId)`.
        abstract isTrusted: bool option with get, set
        /// <summary>Appends and escapes the given string to this markdown string.</summary>
        /// <param name="value">Plain text.</param>
        abstract appendText: value: string -> MarkdownString
        /// <summary>Appends the given string 'as is' to this markdown string.</summary>
        /// <param name="value">Markdown string.</param>
        abstract appendMarkdown: value: string -> MarkdownString
        /// <summary>Appends the given string as codeblock using the provided language.</summary>
        /// <param name="value">A code snippet.</param>
        /// <param name="language">An optional [language identifier](#languages.getLanguages).</param>
        abstract appendCodeblock: value: string * ?language: string -> MarkdownString

    /// The MarkdownString represents human readable text that supports formatting via the
    /// markdown syntax. Standard markdown is supported, also tables, but no embedded html.
    type [<AllowNullLiteral>] MarkdownStringStatic =
        /// <summary>Creates a new markdown string with the given value.</summary>
        /// <param name="value">Optional, initial value.</param>
        [<Emit "new $0($1...)">] abstract Create: ?value: string -> MarkdownString

    type MarkedString =
        U3<MarkdownString, string, obj>

    [<RequireQualifiedAccess; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module MarkedString =
        let ofMarkdownString v: MarkedString = v |> U3.Case1
        let isMarkdownString (v: MarkedString) = match v with U3.Case1 _ -> true | _ -> false
        let asMarkdownString (v: MarkedString) = match v with U3.Case1 o -> Some o | _ -> None
        let ofString v: MarkedString = v |> U3.Case2
        let isString (v: MarkedString) = match v with U3.Case2 _ -> true | _ -> false
        let asString (v: MarkedString) = match v with U3.Case2 o -> Some o | _ -> None
        let ofCase3 v: MarkedString = v |> U3.Case3
        let isCase3 (v: MarkedString) = match v with U3.Case3 _ -> true | _ -> false
        let asCase3 (v: MarkedString) = match v with U3.Case3 o -> Some o | _ -> None

    /// A hover represents additional information for a symbol or word. Hovers are
    /// rendered in a tooltip-like widget.
    type [<AllowNullLiteral>] Hover =
        /// The contents of this hover.
        abstract contents: ResizeArray<MarkedString> with get, set
        /// The range to which this hover applies. When missing, the
        /// editor will use the range at the current position or the
        /// current position itself.
        abstract range: Range option with get, set

    /// A hover represents additional information for a symbol or word. Hovers are
    /// rendered in a tooltip-like widget.
    type [<AllowNullLiteral>] HoverStatic =
        /// <summary>Creates a new hover object.</summary>
        /// <param name="contents">The contents of the hover.</param>
        /// <param name="range">The range to which the hover applies.</param>
        [<Emit "new $0($1...)">] abstract Create: contents: U2<MarkedString, ResizeArray<MarkedString>> * ?range: Range -> Hover

    /// The hover provider interface defines the contract between extensions and
    /// the [hover](https://code.visualstudio.com/docs/editor/intellisense)-feature.
    type [<AllowNullLiteral>] HoverProvider =
        /// <summary>Provide a hover for the given position and document. Multiple hovers at the same
        /// position will be merged by the editor. A hover can have a range which defaults
        /// to the word range at the position when omitted.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="position">The position at which the command was invoked.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideHover: document: TextDocument * position: Position * token: CancellationToken -> ProviderResult<Hover>

    type [<RequireQualifiedAccess>] DocumentHighlightKind =
        | Text = 0
        | Read = 1
        | Write = 2

    /// A document highlight is a range inside a text document which deserves
    /// special attention. Usually a document highlight is visualized by changing
    /// the background color of its range.
    type [<AllowNullLiteral>] DocumentHighlight =
        /// The range this highlight applies to.
        abstract range: Range with get, set
        /// The highlight kind, default is [text](#DocumentHighlightKind.Text).
        abstract kind: DocumentHighlightKind option with get, set

    /// A document highlight is a range inside a text document which deserves
    /// special attention. Usually a document highlight is visualized by changing
    /// the background color of its range.
    type [<AllowNullLiteral>] DocumentHighlightStatic =
        /// <summary>Creates a new document highlight object.</summary>
        /// <param name="range">The range the highlight applies to.</param>
        /// <param name="kind">The highlight kind, default is [text](#DocumentHighlightKind.Text).</param>
        [<Emit "new $0($1...)">] abstract Create: range: Range * ?kind: DocumentHighlightKind -> DocumentHighlight

    /// The document highlight provider interface defines the contract between extensions and
    /// the word-highlight-feature.
    type [<AllowNullLiteral>] DocumentHighlightProvider =
        /// <summary>Provide a set of document highlights, like all occurrences of a variable or
        /// all exit-points of a function.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="position">The position at which the command was invoked.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideDocumentHighlights: document: TextDocument * position: Position * token: CancellationToken -> ProviderResult<ResizeArray<DocumentHighlight>>

    type [<RequireQualifiedAccess>] SymbolKind =
        | File = 0
        | Module = 1
        | Namespace = 2
        | Package = 3
        | Class = 4
        | Method = 5
        | Property = 6
        | Field = 7
        | Constructor = 8
        | Enum = 9
        | Interface = 10
        | Function = 11
        | Variable = 12
        | Constant = 13
        | String = 14
        | Number = 15
        | Boolean = 16
        | Array = 17
        | Object = 18
        | Key = 19
        | Null = 20
        | EnumMember = 21
        | Struct = 22
        | Event = 23
        | Operator = 24
        | TypeParameter = 25

    /// Represents information about programming constructs like variables, classes,
    /// interfaces etc.
    type [<AllowNullLiteral>] SymbolInformation =
        /// The name of this symbol.
        abstract name: string with get, set
        /// The name of the symbol containing this symbol.
        abstract containerName: string with get, set
        /// The kind of this symbol.
        abstract kind: SymbolKind with get, set
        /// The location of this symbol.
        abstract location: Location with get, set

    /// Represents information about programming constructs like variables, classes,
    /// interfaces etc.
    type [<AllowNullLiteral>] SymbolInformationStatic =
        /// <summary>Creates a new symbol information object.</summary>
        /// <param name="name">The name of the symbol.</param>
        /// <param name="kind">The kind of the symbol.</param>
        /// <param name="containerName">The name of the symbol containing the symbol.</param>
        /// <param name="location">The the location of the symbol.</param>
        [<Emit "new $0($1...)">] abstract Create: name: string * kind: SymbolKind * containerName: string * location: Location -> SymbolInformation
        /// <summary>~~Creates a new symbol information object.~~</summary>
        /// <param name="name">The name of the symbol.</param>
        /// <param name="kind">The kind of the symbol.</param>
        /// <param name="range">The range of the location of the symbol.</param>
        /// <param name="uri">The resource of the location of symbol, defaults to the current document.</param>
        /// <param name="containerName">The name of the symbol containing the symbol.</param>
        [<Emit "new $0($1...)">] abstract Create: name: string * kind: SymbolKind * range: Range * ?uri: Uri * ?containerName: string -> SymbolInformation

    /// The document symbol provider interface defines the contract between extensions and
    /// the [go to symbol](https://code.visualstudio.com/docs/editor/editingevolved#_go-to-symbol)-feature.
    type [<AllowNullLiteral>] DocumentSymbolProvider =
        /// <summary>Provide symbol information for the given document.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideDocumentSymbols: document: TextDocument * token: CancellationToken -> ProviderResult<ResizeArray<SymbolInformation>>

    /// The workspace symbol provider interface defines the contract between extensions and
    /// the [symbol search](https://code.visualstudio.com/docs/editor/editingevolved#_open-symbol-by-name)-feature.
    type [<AllowNullLiteral>] WorkspaceSymbolProvider =
        /// <summary>Project-wide search for a symbol matching the given query string. It is up to the provider
        /// how to search given the query string, like substring, indexOf etc. To improve performance implementors can
        /// skip the [location](#SymbolInformation.location) of symbols and implement `resolveWorkspaceSymbol` to do that
        /// later.
        /// 
        /// The `query`-parameter should be interpreted in a *relaxed way* as the editor will apply its own highlighting
        /// and scoring on the results. A good rule of thumb is to match case-insensitive and to simply check that the
        /// characters of *query* appear in their order in a candidate symbol. Don't use prefix, substring, or similar
        /// strict matching.</summary>
        /// <param name="query">A non-empty query string.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideWorkspaceSymbols: query: string * token: CancellationToken -> ProviderResult<ResizeArray<SymbolInformation>>
        /// <summary>Given a symbol fill in its [location](#SymbolInformation.location). This method is called whenever a symbol
        /// is selected in the UI. Providers can implement this method and return incomplete symbols from
        /// [`provideWorkspaceSymbols`](#WorkspaceSymbolProvider.provideWorkspaceSymbols) which often helps to improve
        /// performance.</summary>
        /// <param name="symbol">The symbol that is to be resolved. Guaranteed to be an instance of an object returned from an
        /// earlier call to `provideWorkspaceSymbols`.</param>
        /// <param name="token">A cancellation token.</param>
        abstract resolveWorkspaceSymbol: symbol: SymbolInformation * token: CancellationToken -> ProviderResult<SymbolInformation>

    /// Value-object that contains additional information when
    /// requesting references.
    type [<AllowNullLiteral>] ReferenceContext =
        /// Include the declaration of the current symbol.
        abstract includeDeclaration: bool with get, set

    /// The reference provider interface defines the contract between extensions and
    /// the [find references](https://code.visualstudio.com/docs/editor/editingevolved#_peek)-feature.
    type [<AllowNullLiteral>] ReferenceProvider =
        /// <summary>Provide a set of project-wide references for the given position and document.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="position">The position at which the command was invoked.</param>
        /// <param name="context"></param>
        /// <param name="token">A cancellation token.</param>
        abstract provideReferences: document: TextDocument * position: Position * context: ReferenceContext * token: CancellationToken -> ProviderResult<ResizeArray<Location>>

    /// A text edit represents edits that should be applied
    /// to a document.
    type [<AllowNullLiteral>] TextEdit =
        /// The range this edit applies to.
        abstract range: Range with get, set
        /// The string this edit will insert.
        abstract newText: string with get, set
        /// The eol-sequence used in the document.
        /// 
        /// *Note* that the eol-sequence will be applied to the
        /// whole document.
        abstract newEol: EndOfLine with get, set

    /// A text edit represents edits that should be applied
    /// to a document.
    type [<AllowNullLiteral>] TextEditStatic =
        /// <summary>Utility to create a replace edit.</summary>
        /// <param name="range">A range.</param>
        /// <param name="newText">A string.</param>
        abstract replace: range: Range * newText: string -> TextEdit
        /// <summary>Utility to create an insert edit.</summary>
        /// <param name="position">A position, will become an empty range.</param>
        /// <param name="newText">A string.</param>
        abstract insert: position: Position * newText: string -> TextEdit
        /// <summary>Utility to create a delete edit.</summary>
        /// <param name="range">A range.</param>
        abstract delete: range: Range -> TextEdit
        /// <summary>Utility to create an eol-edit.</summary>
        /// <param name="eol">An eol-sequence</param>
        abstract setEndOfLine: eol: EndOfLine -> TextEdit
        /// <summary>Create a new TextEdit.</summary>
        /// <param name="range">A range.</param>
        /// <param name="newText">A string.</param>
        [<Emit "new $0($1...)">] abstract Create: range: Range * newText: string -> TextEdit

    /// A workspace edit represents textual changes for many documents.
    type [<AllowNullLiteral>] WorkspaceEdit =
        /// The number of affected resources.
        abstract size: float
        /// <summary>Replace the given range with given text for the given resource.</summary>
        /// <param name="uri">A resource identifier.</param>
        /// <param name="range">A range.</param>
        /// <param name="newText">A string.</param>
        abstract replace: uri: Uri * range: Range * newText: string -> unit
        /// <summary>Insert the given text at the given position.</summary>
        /// <param name="uri">A resource identifier.</param>
        /// <param name="position">A position.</param>
        /// <param name="newText">A string.</param>
        abstract insert: uri: Uri * position: Position * newText: string -> unit
        /// <summary>Delete the text at the given range.</summary>
        /// <param name="uri">A resource identifier.</param>
        /// <param name="range">A range.</param>
        abstract delete: uri: Uri * range: Range -> unit
        /// <summary>Check if this edit affects the given resource.</summary>
        /// <param name="uri">A resource identifier.</param>
        abstract has: uri: Uri -> bool
        /// <summary>Set (and replace) text edits for a resource.</summary>
        /// <param name="uri">A resource identifier.</param>
        /// <param name="edits">An array of text edits.</param>
        abstract set: uri: Uri * edits: ResizeArray<TextEdit> -> unit
        /// <summary>Get the text edits for a resource.</summary>
        /// <param name="uri">A resource identifier.</param>
        abstract get: uri: Uri -> ResizeArray<TextEdit>
        /// Get all text edits grouped by resource.
        abstract entries: unit -> ResizeArray<Uri * ResizeArray<TextEdit>>

    /// A workspace edit represents textual changes for many documents.
    type [<AllowNullLiteral>] WorkspaceEditStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> WorkspaceEdit

    /// A snippet string is a template which allows to insert text
    /// and to control the editor cursor when insertion happens.
    /// 
    /// A snippet can define tab stops and placeholders with `$1`, `$2`
    /// and `${3:foo}`. `$0` defines the final tab stop, it defaults to
    /// the end of the snippet. Variables are defined with `$name` and
    /// `${name:default value}`. The full snippet syntax is documented
    /// [here](http://code.visualstudio.com/docs/editor/userdefinedsnippets#_creating-your-own-snippets).
    type [<AllowNullLiteral>] SnippetString =
        /// The snippet string.
        abstract value: string with get, set
        /// <summary>Builder-function that appends the given string to
        /// the [`value`](#SnippetString.value) of this snippet string.</summary>
        /// <param name="string">A value to append 'as given'. The string will be escaped.</param>
        abstract appendText: string: string -> SnippetString
        /// <summary>Builder-function that appends a tabstop (`$1`, `$2` etc) to
        /// the [`value`](#SnippetString.value) of this snippet string.</summary>
        /// <param name="number">The number of this tabstop, defaults to an auto-incremet
        /// value starting at 1.</param>
        abstract appendTabstop: ?number: float -> SnippetString
        /// <summary>Builder-function that appends a placeholder (`${1:value}`) to
        /// the [`value`](#SnippetString.value) of this snippet string.</summary>
        /// <param name="value">The value of this placeholder - either a string or a function
        /// with which a nested snippet can be created.</param>
        /// <param name="number">The number of this tabstop, defaults to an auto-incremet
        /// value starting at 1.</param>
        abstract appendPlaceholder: value: U2<string, (SnippetString -> obj option)> * ?number: float -> SnippetString
        /// <summary>Builder-function that appends a variable (`${VAR}`) to
        /// the [`value`](#SnippetString.value) of this snippet string.</summary>
        /// <param name="name">The name of the variable - excluding the `$`.</param>
        /// <param name="defaultValue">The default value which is used when the variable name cannot
        /// be resolved - either a string or a function with which a nested snippet can be created.</param>
        abstract appendVariable: name: string * defaultValue: U2<string, (SnippetString -> obj option)> -> SnippetString

    /// A snippet string is a template which allows to insert text
    /// and to control the editor cursor when insertion happens.
    /// 
    /// A snippet can define tab stops and placeholders with `$1`, `$2`
    /// and `${3:foo}`. `$0` defines the final tab stop, it defaults to
    /// the end of the snippet. Variables are defined with `$name` and
    /// `${name:default value}`. The full snippet syntax is documented
    /// [here](http://code.visualstudio.com/docs/editor/userdefinedsnippets#_creating-your-own-snippets).
    type [<AllowNullLiteral>] SnippetStringStatic =
        [<Emit "new $0($1...)">] abstract Create: ?value: string -> SnippetString

    /// The rename provider interface defines the contract between extensions and
    /// the [rename](https://code.visualstudio.com/docs/editor/editingevolved#_rename-symbol)-feature.
    type [<AllowNullLiteral>] RenameProvider =
        /// <summary>Provide an edit that describes changes that have to be made to one
        /// or many resources to rename a symbol to a different name.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="position">The position at which the command was invoked.</param>
        /// <param name="newName">The new name of the symbol. If the given name is not valid, the provider must return a rejected promise.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideRenameEdits: document: TextDocument * position: Position * newName: string * token: CancellationToken -> ProviderResult<WorkspaceEdit>

    /// Value-object describing what options formatting should use.
    type [<AllowNullLiteral>] FormattingOptions =
        /// Size of a tab in spaces.
        abstract tabSize: float with get, set
        /// Prefer spaces over tabs.
        abstract insertSpaces: bool with get, set
        /// Signature for further properties.
        [<Emit "$0[$1]{{=$2}}">] abstract Item: key: string -> U3<bool, float, string> with get, set

    /// The document formatting provider interface defines the contract between extensions and
    /// the formatting-feature.
    type [<AllowNullLiteral>] DocumentFormattingEditProvider =
        /// <summary>Provide formatting edits for a whole document.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="options">Options controlling formatting.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideDocumentFormattingEdits: document: TextDocument * options: FormattingOptions * token: CancellationToken -> ProviderResult<ResizeArray<TextEdit>>

    /// The document formatting provider interface defines the contract between extensions and
    /// the formatting-feature.
    type [<AllowNullLiteral>] DocumentRangeFormattingEditProvider =
        /// <summary>Provide formatting edits for a range in a document.
        /// 
        /// The given range is a hint and providers can decide to format a smaller
        /// or larger range. Often this is done by adjusting the start and end
        /// of the range to full syntax nodes.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="range">The range which should be formatted.</param>
        /// <param name="options">Options controlling formatting.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideDocumentRangeFormattingEdits: document: TextDocument * range: Range * options: FormattingOptions * token: CancellationToken -> ProviderResult<ResizeArray<TextEdit>>

    /// The document formatting provider interface defines the contract between extensions and
    /// the formatting-feature.
    type [<AllowNullLiteral>] OnTypeFormattingEditProvider =
        /// <summary>Provide formatting edits after a character has been typed.
        /// 
        /// The given position and character should hint to the provider
        /// what range the position to expand to, like find the matching `{`
        /// when `}` has been entered.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="position">The position at which the command was invoked.</param>
        /// <param name="ch">The character that has been typed.</param>
        /// <param name="options">Options controlling formatting.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideOnTypeFormattingEdits: document: TextDocument * position: Position * ch: string * options: FormattingOptions * token: CancellationToken -> ProviderResult<ResizeArray<TextEdit>>

    /// Represents a parameter of a callable-signature. A parameter can
    /// have a label and a doc-comment.
    type [<AllowNullLiteral>] ParameterInformation =
        /// The label of this signature. Will be shown in
        /// the UI.
        abstract label: string with get, set
        /// The human-readable doc-comment of this signature. Will be shown
        /// in the UI but can be omitted.
        abstract documentation: U2<string, MarkdownString> option with get, set

    /// Represents a parameter of a callable-signature. A parameter can
    /// have a label and a doc-comment.
    type [<AllowNullLiteral>] ParameterInformationStatic =
        /// <summary>Creates a new parameter information object.</summary>
        /// <param name="label">A label string.</param>
        /// <param name="documentation">A doc string.</param>
        [<Emit "new $0($1...)">] abstract Create: label: string * ?documentation: U2<string, MarkdownString> -> ParameterInformation

    /// Represents the signature of something callable. A signature
    /// can have a label, like a function-name, a doc-comment, and
    /// a set of parameters.
    type [<AllowNullLiteral>] SignatureInformation =
        /// The label of this signature. Will be shown in
        /// the UI.
        abstract label: string with get, set
        /// The human-readable doc-comment of this signature. Will be shown
        /// in the UI but can be omitted.
        abstract documentation: U2<string, MarkdownString> option with get, set
        /// The parameters of this signature.
        abstract parameters: ResizeArray<ParameterInformation> with get, set

    /// Represents the signature of something callable. A signature
    /// can have a label, like a function-name, a doc-comment, and
    /// a set of parameters.
    type [<AllowNullLiteral>] SignatureInformationStatic =
        /// <summary>Creates a new signature information object.</summary>
        /// <param name="label">A label string.</param>
        /// <param name="documentation">A doc string.</param>
        [<Emit "new $0($1...)">] abstract Create: label: string * ?documentation: U2<string, MarkdownString> -> SignatureInformation

    /// Signature help represents the signature of something
    /// callable. There can be multiple signatures but only one
    /// active and only one active parameter.
    type [<AllowNullLiteral>] SignatureHelp =
        /// One or more signatures.
        abstract signatures: ResizeArray<SignatureInformation> with get, set
        /// The active signature.
        abstract activeSignature: float with get, set
        /// The active parameter of the active signature.
        abstract activeParameter: float with get, set

    /// Signature help represents the signature of something
    /// callable. There can be multiple signatures but only one
    /// active and only one active parameter.
    type [<AllowNullLiteral>] SignatureHelpStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> SignatureHelp

    /// The signature help provider interface defines the contract between extensions and
    /// the [parameter hints](https://code.visualstudio.com/docs/editor/intellisense)-feature.
    type [<AllowNullLiteral>] SignatureHelpProvider =
        /// <summary>Provide help for the signature at the given position and document.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="position">The position at which the command was invoked.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideSignatureHelp: document: TextDocument * position: Position * token: CancellationToken -> ProviderResult<SignatureHelp>

    type [<RequireQualifiedAccess>] CompletionItemKind =
        | Text = 0
        | Method = 1
        | Function = 2
        | Constructor = 3
        | Field = 4
        | Variable = 5
        | Class = 6
        | Interface = 7
        | Module = 8
        | Property = 9
        | Unit = 10
        | Value = 11
        | Enum = 12
        | Keyword = 13
        | Snippet = 14
        | Color = 15
        | Reference = 17
        | File = 16
        | Folder = 18
        | EnumMember = 19
        | Constant = 20
        | Struct = 21
        | Event = 22
        | Operator = 23
        | TypeParameter = 24

    /// A completion item represents a text snippet that is proposed to complete text that is being typed.
    /// 
    /// It is suffient to create a completion item from just a [label](#CompletionItem.label). In that
    /// case the completion item will replace the [word](#TextDocument.getWordRangeAtPosition)
    /// until the cursor with the given label or [insertText](#CompletionItem.insertText). Otherwise the
    /// the given [edit](#CompletionItem.textEdit) is used.
    /// 
    /// When selecting a completion item in the editor its defined or synthesized text edit will be applied
    /// to *all* cursors/selections whereas [additionalTextEdits](CompletionItem.additionalTextEdits) will be
    /// applied as provided.
    type [<AllowNullLiteral>] CompletionItem =
        /// The label of this completion item. By default
        /// this is also the text that is inserted when selecting
        /// this completion.
        abstract label: string with get, set
        /// The kind of this completion item. Based on the kind
        /// an icon is chosen by the editor.
        abstract kind: CompletionItemKind option with get, set
        /// A human-readable string with additional information
        /// about this item, like type or symbol information.
        abstract detail: string option with get, set
        /// A human-readable string that represents a doc-comment.
        abstract documentation: U2<string, MarkdownString> option with get, set
        /// A string that should be used when comparing this item
        /// with other items. When `falsy` the [label](#CompletionItem.label)
        /// is used.
        abstract sortText: string option with get, set
        /// A string that should be used when filtering a set of
        /// completion items. When `falsy` the [label](#CompletionItem.label)
        /// is used.
        abstract filterText: string option with get, set
        /// A string or snippet that should be inserted in a document when selecting
        /// this completion. When `falsy` the [label](#CompletionItem.label)
        /// is used.
        abstract insertText: U2<string, SnippetString> option with get, set
        /// A range of text that should be replaced by this completion item.
        /// 
        /// Defaults to a range from the start of the [current word](#TextDocument.getWordRangeAtPosition) to the
        /// current position.
        /// 
        /// *Note:* The range must be a [single line](#Range.isSingleLine) and it must
        /// [contain](#Range.contains) the position at which completion has been [requested](#CompletionItemProvider.provideCompletionItems).
        abstract range: Range option with get, set
        /// An optional set of characters that when pressed while this completion is active will accept it first and
        /// then type that character. *Note* that all commit characters should have `length=1` and that superfluous
        /// characters will be ignored.
        abstract commitCharacters: ResizeArray<string> option with get, set
        abstract textEdit: TextEdit option with get, set
        /// An optional array of additional [text edits](#TextEdit) that are applied when
        /// selecting this completion. Edits must not overlap with the main [edit](#CompletionItem.textEdit)
        /// nor with themselves.
        abstract additionalTextEdits: ResizeArray<TextEdit> option with get, set
        /// An optional [command](#Command) that is executed *after* inserting this completion. *Note* that
        /// additional modifications to the current document should be described with the
        /// [additionalTextEdits](#CompletionItem.additionalTextEdits)-property.
        abstract command: Command option with get, set

    /// A completion item represents a text snippet that is proposed to complete text that is being typed.
    /// 
    /// It is suffient to create a completion item from just a [label](#CompletionItem.label). In that
    /// case the completion item will replace the [word](#TextDocument.getWordRangeAtPosition)
    /// until the cursor with the given label or [insertText](#CompletionItem.insertText). Otherwise the
    /// the given [edit](#CompletionItem.textEdit) is used.
    /// 
    /// When selecting a completion item in the editor its defined or synthesized text edit will be applied
    /// to *all* cursors/selections whereas [additionalTextEdits](CompletionItem.additionalTextEdits) will be
    /// applied as provided.
    type [<AllowNullLiteral>] CompletionItemStatic =
        /// <summary>Creates a new completion item.
        /// 
        /// Completion items must have at least a [label](#CompletionItem.label) which then
        /// will be used as insert text as well as for sorting and filtering.</summary>
        /// <param name="label">The label of the completion.</param>
        /// <param name="kind">The [kind](#CompletionItemKind) of the completion.</param>
        [<Emit "new $0($1...)">] abstract Create: label: string * ?kind: CompletionItemKind -> CompletionItem

    /// Represents a collection of [completion items](#CompletionItem) to be presented
    /// in the editor.
    type [<AllowNullLiteral>] CompletionList =
        /// This list it not complete. Further typing should result in recomputing
        /// this list.
        abstract isIncomplete: bool option with get, set
        /// The completion items.
        abstract items: ResizeArray<CompletionItem> with get, set

    /// Represents a collection of [completion items](#CompletionItem) to be presented
    /// in the editor.
    type [<AllowNullLiteral>] CompletionListStatic =
        /// <summary>Creates a new completion list.</summary>
        /// <param name="items">The completion items.</param>
        /// <param name="isIncomplete">The list is not complete.</param>
        [<Emit "new $0($1...)">] abstract Create: ?items: ResizeArray<CompletionItem> * ?isIncomplete: bool -> CompletionList

    type [<RequireQualifiedAccess>] CompletionTriggerKind =
        | Invoke = 0
        | TriggerCharacter = 1

    /// Contains additional information about the context in which
    /// [completion provider](#CompletionItemProvider.provideCompletionItems) is triggered.
    type [<AllowNullLiteral>] CompletionContext =
        /// How the completion was triggered.
        abstract triggerKind: CompletionTriggerKind
        /// Character that triggered the completion item provider.
        /// 
        /// `undefined` if provider was not triggered by a character.
        /// 
        /// The trigger character is already in the document when the completion provider is triggered.
        abstract triggerCharacter: string option

    /// The completion item provider interface defines the contract between extensions and
    /// [IntelliSense](https://code.visualstudio.com/docs/editor/intellisense).
    /// 
    /// When computing *complete* completion items is expensive, providers can optionally implement
    /// the `resolveCompletionItem`-function. In that case it is enough to return completion
    /// items with a [label](#CompletionItem.label) from the
    /// [provideCompletionItems](#CompletionItemProvider.provideCompletionItems)-function. Subsequently,
    /// when a completion item is shown in the UI and gains focus this provider is asked to resolve
    /// the item, like adding [doc-comment](#CompletionItem.documentation) or [details](#CompletionItem.detail).
    /// 
    /// Providers are asked for completions either explicitly by a user gesture or -depending on the configuration-
    /// implicitly when typing words or trigger characters.
    type [<AllowNullLiteral>] CompletionItemProvider =
        /// <summary>Provide completion items for the given position and document.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="position">The position at which the command was invoked.</param>
        /// <param name="token">A cancellation token.</param>
        /// <param name="context">How the completion was triggered.</param>
        abstract provideCompletionItems: document: TextDocument * position: Position * token: CancellationToken * context: CompletionContext -> ProviderResult<U2<ResizeArray<CompletionItem>, CompletionList>>
        /// <summary>Given a completion item fill in more data, like [doc-comment](#CompletionItem.documentation)
        /// or [details](#CompletionItem.detail).
        /// 
        /// The editor will only resolve a completion item once.</summary>
        /// <param name="item">A completion item currently active in the UI.</param>
        /// <param name="token">A cancellation token.</param>
        abstract resolveCompletionItem: item: CompletionItem * token: CancellationToken -> ProviderResult<CompletionItem>

    /// A document link is a range in a text document that links to an internal or external resource, like another
    /// text document or a web site.
    type [<AllowNullLiteral>] DocumentLink =
        /// The range this link applies to.
        abstract range: Range with get, set
        /// The uri this link points to.
        abstract target: Uri option with get, set

    /// A document link is a range in a text document that links to an internal or external resource, like another
    /// text document or a web site.
    type [<AllowNullLiteral>] DocumentLinkStatic =
        /// <summary>Creates a new document link.</summary>
        /// <param name="range">The range the document link applies to. Must not be empty.</param>
        /// <param name="target">The uri the document link points to.</param>
        [<Emit "new $0($1...)">] abstract Create: range: Range * ?target: Uri -> DocumentLink

    /// The document link provider defines the contract between extensions and feature of showing
    /// links in the editor.
    type [<AllowNullLiteral>] DocumentLinkProvider =
        /// <summary>Provide links for the given document. Note that the editor ships with a default provider that detects
        /// `http(s)` and `file` links.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideDocumentLinks: document: TextDocument * token: CancellationToken -> ProviderResult<ResizeArray<DocumentLink>>
        /// <summary>Given a link fill in its [target](#DocumentLink.target). This method is called when an incomplete
        /// link is selected in the UI. Providers can implement this method and return incomple links
        /// (without target) from the [`provideDocumentLinks`](#DocumentLinkProvider.provideDocumentLinks) method which
        /// often helps to improve performance.</summary>
        /// <param name="link">The link that is to be resolved.</param>
        /// <param name="token">A cancellation token.</param>
        abstract resolveDocumentLink: link: DocumentLink * token: CancellationToken -> ProviderResult<DocumentLink>

    /// Represents a color in RGBA space.
    type [<AllowNullLiteral>] Color =
        /// The red component of this color in the range [0-1].
        abstract red: float
        /// The green component of this color in the range [0-1].
        abstract green: float
        /// The blue component of this color in the range [0-1].
        abstract blue: float
        /// The alpha component of this color in the range [0-1].
        abstract alpha: float

    /// Represents a color in RGBA space.
    type [<AllowNullLiteral>] ColorStatic =
        /// <summary>Creates a new color instance.</summary>
        /// <param name="red">The red component.</param>
        /// <param name="green">The green component.</param>
        /// <param name="blue">The bluew component.</param>
        /// <param name="alpha">The alpha component.</param>
        [<Emit "new $0($1...)">] abstract Create: red: float * green: float * blue: float * alpha: float -> Color

    /// Represents a color range from a document.
    type [<AllowNullLiteral>] ColorInformation =
        /// The range in the document where this color appers.
        abstract range: Range with get, set
        /// The actual color value for this color range.
        abstract color: Color with get, set

    /// Represents a color range from a document.
    type [<AllowNullLiteral>] ColorInformationStatic =
        /// <summary>Creates a new color range.</summary>
        /// <param name="range">The range the color appears in. Must not be empty.</param>
        /// <param name="color">The value of the color.</param>
        [<Emit "new $0($1...)">] abstract Create: range: Range * color: Color -> ColorInformation

    /// A color presentation object describes how a [`color`](#Color) should be represented as text and what
    /// edits are required to refer to it from source code.
    /// 
    /// For some languages one color can have multiple presentations, e.g. css can represent the color red with
    /// the constant `Red`, the hex-value `#ff0000`, or in rgba and hsla forms. In csharp other representations
    /// apply, e.g `System.Drawing.Color.Red`.
    type [<AllowNullLiteral>] ColorPresentation =
        /// The label of this color presentation. It will be shown on the color
        /// picker header. By default this is also the text that is inserted when selecting
        /// this color presentation.
        abstract label: string with get, set
        /// An [edit](#TextEdit) which is applied to a document when selecting
        /// this presentation for the color.  When `falsy` the [label](#ColorPresentation.label)
        /// is used.
        abstract textEdit: TextEdit option with get, set
        /// An optional array of additional [text edits](#TextEdit) that are applied when
        /// selecting this color presentation. Edits must not overlap with the main [edit](#ColorPresentation.textEdit) nor with themselves.
        abstract additionalTextEdits: ResizeArray<TextEdit> option with get, set

    /// A color presentation object describes how a [`color`](#Color) should be represented as text and what
    /// edits are required to refer to it from source code.
    /// 
    /// For some languages one color can have multiple presentations, e.g. css can represent the color red with
    /// the constant `Red`, the hex-value `#ff0000`, or in rgba and hsla forms. In csharp other representations
    /// apply, e.g `System.Drawing.Color.Red`.
    type [<AllowNullLiteral>] ColorPresentationStatic =
        /// <summary>Creates a new color presentation.</summary>
        /// <param name="label">The label of this color presentation.</param>
        [<Emit "new $0($1...)">] abstract Create: label: string -> ColorPresentation

    /// The document color provider defines the contract between extensions and feature of
    /// picking and modifying colors in the editor.
    type [<AllowNullLiteral>] DocumentColorProvider =
        /// <summary>Provide colors for the given document.</summary>
        /// <param name="document">The document in which the command was invoked.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideDocumentColors: document: TextDocument * token: CancellationToken -> ProviderResult<ResizeArray<ColorInformation>>
        /// <summary>Provide [representations](#ColorPresentation) for a color.</summary>
        /// <param name="color">The color to show and insert.</param>
        /// <param name="context">A context object with additional information</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideColorPresentations: color: Color * context: DocumentColorProviderProvideColorPresentationsContext * token: CancellationToken -> ProviderResult<ResizeArray<ColorPresentation>>

    type [<AllowNullLiteral>] DocumentColorProviderProvideColorPresentationsContext =
        abstract document: TextDocument with get, set
        abstract range: Range with get, set

    type CharacterPair =
        string * string

    /// Describes how comments for a language work.
    type [<AllowNullLiteral>] CommentRule =
        /// The line comment token, like `// this is a comment`
        abstract lineComment: string option with get, set
        /// The block comment character pair, like `/* block comment *&#47;`
        abstract blockComment: CharacterPair option with get, set

    /// Describes indentation rules for a language.
    type [<AllowNullLiteral>] IndentationRule =
        /// If a line matches this pattern, then all the lines after it should be unindendented once (until another rule matches).
        abstract decreaseIndentPattern: RegExp with get, set
        /// If a line matches this pattern, then all the lines after it should be indented once (until another rule matches).
        abstract increaseIndentPattern: RegExp with get, set
        /// If a line matches this pattern, then **only the next line** after it should be indented once.
        abstract indentNextLinePattern: RegExp option with get, set
        /// If a line matches this pattern, then its indentation should not be changed and it should not be evaluated against the other rules.
        abstract unIndentedLinePattern: RegExp option with get, set

    type [<RequireQualifiedAccess>] IndentAction =
        | None = 0
        | Indent = 1
        | IndentOutdent = 2
        | Outdent = 3

    /// Describes what to do when pressing Enter.
    type [<AllowNullLiteral>] EnterAction =
        /// Describe what to do with the indentation.
        abstract indentAction: IndentAction with get, set
        /// Describes text to be appended after the new line and after the indentation.
        abstract appendText: string option with get, set
        /// Describes the number of characters to remove from the new line's indentation.
        abstract removeText: float option with get, set

    /// Describes a rule to be evaluated when pressing Enter.
    type [<AllowNullLiteral>] OnEnterRule =
        /// This rule will only execute if the text before the cursor matches this regular expression.
        abstract beforeText: RegExp with get, set
        /// This rule will only execute if the text after the cursor matches this regular expression.
        abstract afterText: RegExp option with get, set
        /// The action to execute.
        abstract action: EnterAction with get, set

    /// The language configuration interfaces defines the contract between extensions
    /// and various editor features, like automatic bracket insertion, automatic indentation etc.
    type [<AllowNullLiteral>] LanguageConfiguration =
        /// The language's comment settings.
        abstract comments: CommentRule option with get, set
        /// The language's brackets.
        /// This configuration implicitly affects pressing Enter around these brackets.
        abstract brackets: ResizeArray<CharacterPair> option with get, set
        /// The language's word definition.
        /// If the language supports Unicode identifiers (e.g. JavaScript), it is preferable
        /// to provide a word definition that uses exclusion of known separators.
        /// e.g.: A regex that matches anything except known separators (and dot is allowed to occur in a floating point number):
        ///    /(-?\d*\.\d\w*)|([^\`\~\!\@\#\%\^\&\*\(\)\-\=\+\[\{\]\}\\\|\;\:\'\"\,\.\<\>\/\?\s]+)/g
        abstract wordPattern: RegExp option with get, set
        /// The language's indentation settings.
        abstract indentationRules: IndentationRule option with get, set
        /// The language's rules to be evaluated when pressing Enter.
        abstract onEnterRules: ResizeArray<OnEnterRule> option with get, set
        /// **Deprecated** Do not use.
        abstract __electricCharacterSupport: obj option with get, set
        /// **Deprecated** Do not use.
        abstract __characterPairSupport: obj option with get, set

    type [<RequireQualifiedAccess>] ConfigurationTarget =
        | Global = 1
        | Workspace = 2
        | WorkspaceFolder = 3

    /// Represents the configuration. It is a merged view of
    /// 
    /// - Default configuration
    /// - Global configuration
    /// - Workspace configuration (if available)
    /// - Workspace folder configuration of the requested resource (if available)
    /// 
    /// *Global configuration* comes from User Settings and shadows Defaults.
    /// 
    /// *Workspace configuration* comes from Workspace Settings and shadows Global configuration.
    /// 
    /// *Workspace Folder configuration* comes from `.vscode` folder under one of the [workspace folders](#workspace.workspaceFolders).
    /// 
    /// *Note:* Workspace and Workspace Folder configurations contains `launch` and `tasks` settings. Their basename will be
    /// part of the section identifier. The following snippets shows how to retrieve all configurations
    /// from `launch.json`:
    /// 
    /// ```ts
    /// // launch.json configuration
    /// const config = workspace.getConfiguration('launch', vscode.window.activeTextEditor.document.uri);
    /// 
    /// // retrieve values
    /// const values = config.get('configurations');
    /// ```
    /// 
    /// Refer to [Settings](https://code.visualstudio.com/docs/getstarted/settings) for more information.
    type [<AllowNullLiteral>] WorkspaceConfiguration =
        /// <summary>Return a value from this configuration.</summary>
        /// <param name="section">Configuration name, supports _dotted_ names.</param>
        abstract get: section: string -> 'T option
        /// <summary>Return a value from this configuration.</summary>
        /// <param name="section">Configuration name, supports _dotted_ names.</param>
        /// <param name="defaultValue">A value should be returned when no value could be found, is `undefined`.</param>
        abstract get: section: string * defaultValue: 'T -> 'T
        /// <summary>Check if this configuration has a certain value.</summary>
        /// <param name="section">Configuration name, supports _dotted_ names.</param>
        abstract has: section: string -> bool
        /// <summary>Retrieve all information about a configuration setting. A configuration value
        /// often consists of a *default* value, a global or installation-wide value,
        /// a workspace-specific value and a folder-specific value.
        /// 
        /// The *effective* value (returned by [`get`](#WorkspaceConfiguration.get))
        /// is computed like this: `defaultValue` overwritten by `globalValue`,
        /// `globalValue` overwritten by `workspaceValue`. `workspaceValue` overwritten by `workspaceFolderValue`.
        /// Refer to [Settings Inheritence](https://code.visualstudio.com/docs/getstarted/settings)
        /// for more information.
        /// 
        /// *Note:* The configuration name must denote a leaf in the configuration tree
        /// (`editor.fontSize` vs `editor`) otherwise no result is returned.</summary>
        /// <param name="section">Configuration name, supports _dotted_ names.</param>
        abstract inspect: section: string -> obj option
        /// <summary>Update a configuration value. The updated configuration values are persisted.
        /// 
        /// A value can be changed in
        /// 
        /// - [Global configuration](#ConfigurationTarget.Global): Changes the value for all instances of the editor.
        /// - [Workspace configuration](#ConfigurationTarget.Workspace): Changes the value for current workspace, if available.
        /// - [Workspace folder configuration](#ConfigurationTarget.WorkspaceFolder): Changes the value for the
        /// [Workspace folder](#workspace.workspaceFolders) to which the current [configuration](#WorkspaceConfiguration) is scoped to.
        /// 
        /// *Note 1:* Setting a global value in the presence of a more specific workspace value
        /// has no observable effect in that workspace, but in others. Setting a workspace value
        /// in the presence of a more specific folder value has no observable effect for the resources
        /// under respective [folder](#workspace.workspaceFolders), but in others. Refer to
        /// [Settings Inheritence](https://code.visualstudio.com/docs/getstarted/settings) for more information.
        /// 
        /// *Note 2:* To remove a configuration value use `undefined`, like so: `config.update('somekey', undefined)`
        /// 
        /// Will throw error when
        /// - Writing a configuration which is not registered.
        /// - Writing a configuration to workspace or folder target when no workspace is opened
        /// - Writing a configuration to folder target when there is no folder settings
        /// - Writing to folder target without passing a resource when getting the configuration (`workspace.getConfiguration(section, resource)`)
        /// - Writing a window configuration to folder target</summary>
        /// <param name="section">Configuration name, supports _dotted_ names.</param>
        /// <param name="value">The new value.</param>
        /// <param name="configurationTarget">The [configuration target](#ConfigurationTarget) or a boolean value.
        /// - If `true` configuration target is `ConfigurationTarget.Global`.
        /// - If `false` configuration target is `ConfigurationTarget.Workspace`.
        /// - If `undefined` or `null` configuration target is
        /// `ConfigurationTarget.WorkspaceFolder` when configuration is resource specific
        /// `ConfigurationTarget.Workspace` otherwise.</param>
        abstract update: section: string * value: obj option * ?configurationTarget: U2<ConfigurationTarget, bool> -> Thenable<unit>
        /// Readable dictionary that backs this configuration.
        [<Emit "$0[$1]{{=$2}}">] abstract Item: key: string -> obj option

    /// Represents a location inside a resource, such as a line
    /// inside a text file.
    type [<AllowNullLiteral>] Location =
        /// The resource identifier of this location.
        abstract uri: Uri with get, set
        /// The document range of this locations.
        abstract range: Range with get, set

    /// Represents a location inside a resource, such as a line
    /// inside a text file.
    type [<AllowNullLiteral>] LocationStatic =
        /// <summary>Creates a new location object.</summary>
        /// <param name="uri">The resource identifier.</param>
        /// <param name="rangeOrPosition">The range or position. Positions will be converted to an empty range.</param>
        [<Emit "new $0($1...)">] abstract Create: uri: Uri * rangeOrPosition: U2<Range, Position> -> Location

    type [<RequireQualifiedAccess>] DiagnosticSeverity =
        | Error = 0
        | Warning = 1
        | Information = 2
        | Hint = 3

    /// Represents a diagnostic, such as a compiler error or warning. Diagnostic objects
    /// are only valid in the scope of a file.
    type [<AllowNullLiteral>] Diagnostic =
        /// The range to which this diagnostic applies.
        abstract range: Range with get, set
        /// The human-readable message.
        abstract message: string with get, set
        /// A human-readable string describing the source of this
        /// diagnostic, e.g. 'typescript' or 'super lint'.
        abstract source: string with get, set
        /// The severity, default is [error](#DiagnosticSeverity.Error).
        abstract severity: DiagnosticSeverity with get, set
        /// A code or identifier for this diagnostics. Will not be surfaced
        /// to the user, but should be used for later processing, e.g. when
        /// providing [code actions](#CodeActionContext).
        abstract code: U2<string, float> with get, set

    /// Represents a diagnostic, such as a compiler error or warning. Diagnostic objects
    /// are only valid in the scope of a file.
    type [<AllowNullLiteral>] DiagnosticStatic =
        /// <summary>Creates a new diagnostic object.</summary>
        /// <param name="range">The range to which this diagnostic applies.</param>
        /// <param name="message">The human-readable message.</param>
        /// <param name="severity">The severity, default is [error](#DiagnosticSeverity.Error).</param>
        [<Emit "new $0($1...)">] abstract Create: range: Range * message: string * ?severity: DiagnosticSeverity -> Diagnostic

    /// A diagnostics collection is a container that manages a set of
    /// [diagnostics](#Diagnostic). Diagnostics are always scopes to a
    /// diagnostics collection and a resource.
    /// 
    /// To get an instance of a `DiagnosticCollection` use
    /// [createDiagnosticCollection](#languages.createDiagnosticCollection).
    type [<AllowNullLiteral>] DiagnosticCollection =
        /// The name of this diagnostic collection, for instance `typescript`. Every diagnostic
        /// from this collection will be associated with this name. Also, the task framework uses this
        /// name when defining [problem matchers](https://code.visualstudio.com/docs/editor/tasks#_defining-a-problem-matcher).
        abstract name: string
        /// <summary>Assign diagnostics for given resource. Will replace
        /// existing diagnostics for that resource.</summary>
        /// <param name="uri">A resource identifier.</param>
        /// <param name="diagnostics">Array of diagnostics or `undefined`</param>
        abstract set: uri: Uri * diagnostics: ResizeArray<Diagnostic> option -> unit
        /// <summary>Replace all entries in this collection.
        /// 
        /// Diagnostics of multiple tuples of the same uri will be merged, e.g
        /// `[[file1, [d1]], [file1, [d2]]]` is equivalent to `[[file1, [d1, d2]]]`.
        /// If a diagnostics item is `undefined` as in `[file1, undefined]`
        /// all previous but not subsequent diagnostics are removed.</summary>
        /// <param name="entries">An array of tuples, like `[[file1, [d1, d2]], [file2, [d3, d4, d5]]]`, or `undefined`.</param>
        abstract set: entries: ResizeArray<Uri * ResizeArray<Diagnostic> option> -> unit
        /// <summary>Remove all diagnostics from this collection that belong
        /// to the provided `uri`. The same as `#set(uri, undefined)`.</summary>
        /// <param name="uri">A resource identifier.</param>
        abstract delete: uri: Uri -> unit
        /// Remove all diagnostics from this collection. The same
        /// as calling `#set(undefined)`;
        abstract clear: unit -> unit
        /// <summary>Iterate over each entry in this collection.</summary>
        /// <param name="callback">Function to execute for each entry.</param>
        /// <param name="thisArg">The `this` context used when invoking the handler function.</param>
        abstract forEach: callback: (Uri -> ResizeArray<Diagnostic> -> DiagnosticCollection -> obj option) * ?thisArg: obj option -> unit
        /// <summary>Get the diagnostics for a given resource. *Note* that you cannot
        /// modify the diagnostics-array returned from this call.</summary>
        /// <param name="uri">A resource identifier.</param>
        abstract get: uri: Uri -> ResizeArray<Diagnostic> option
        /// <summary>Check if this collection contains diagnostics for a
        /// given resource.</summary>
        /// <param name="uri">A resource identifier.</param>
        abstract has: uri: Uri -> bool
        /// Dispose and free associated resources. Calls
        /// [clear](#DiagnosticCollection.clear).
        abstract dispose: unit -> unit

    type [<RequireQualifiedAccess>] ViewColumn =
        | Active = -1
        | One = 1
        | Two = 2
        | Three = 3

    /// An output channel is a container for readonly textual information.
    /// 
    /// To get an instance of an `OutputChannel` use
    /// [createOutputChannel](#window.createOutputChannel).
    type [<AllowNullLiteral>] OutputChannel =
        /// The human-readable name of this output channel.
        abstract name: string
        /// <summary>Append the given value to the channel.</summary>
        /// <param name="value">A string, falsy values will not be printed.</param>
        abstract append: value: string -> unit
        /// <summary>Append the given value and a line feed character
        /// to the channel.</summary>
        /// <param name="value">A string, falsy values will be printed.</param>
        abstract appendLine: value: string -> unit
        /// Removes all output from the channel.
        abstract clear: unit -> unit
        /// <summary>Reveal this channel in the UI.</summary>
        /// <param name="preserveFocus">When `true` the channel will not take focus.</param>
        abstract show: ?preserveFocus: bool -> unit
        /// <summary>~~Reveal this channel in the UI.~~</summary>
        /// <param name="column">This argument is **deprecated** and will be ignored.</param>
        /// <param name="preserveFocus">When `true` the channel will not take focus.</param>
        abstract show: ?column: ViewColumn * ?preserveFocus: bool -> unit
        /// Hide this channel from the UI.
        abstract hide: unit -> unit
        /// Dispose and free associated resources.
        abstract dispose: unit -> unit

    type [<RequireQualifiedAccess>] StatusBarAlignment =
        | Left = 1
        | Right = 2

    /// A status bar item is a status bar contribution that can
    /// show text and icons and run a command on click.
    type [<AllowNullLiteral>] StatusBarItem =
        /// The alignment of this item.
        abstract alignment: StatusBarAlignment
        /// The priority of this item. Higher value means the item should
        /// be shown more to the left.
        abstract priority: float
        /// The text to show for the entry. You can embed icons in the text by leveraging the syntax:
        /// 
        /// `My text $(icon-name) contains icons like $(icon'name) this one.`
        /// 
        /// Where the icon-name is taken from the [octicon](https://octicons.github.com) icon set, e.g.
        /// `light-bulb`, `thumbsup`, `zap` etc.
        abstract text: string with get, set
        /// The tooltip text when you hover over this entry.
        abstract tooltip: string option with get, set
        /// The foreground color for this entry.
        abstract color: U2<string, ThemeColor> option with get, set
        /// The identifier of a command to run on click. The command must be
        /// [known](#commands.getCommands).
        abstract command: string option with get, set
        /// Shows the entry in the status bar.
        abstract show: unit -> unit
        /// Hide the entry in the status bar.
        abstract hide: unit -> unit
        /// Dispose and free associated resources. Call
        /// [hide](#StatusBarItem.hide).
        abstract dispose: unit -> unit

    /// Defines a generalized way of reporting progress updates.
    type [<AllowNullLiteral>] Progress<'T> =
        /// <summary>Report a progress update.</summary>
        /// <param name="value">A progress item, like a message or an updated percentage value</param>
        abstract report: value: 'T -> unit

    /// An individual terminal instance within the integrated terminal.
    type [<AllowNullLiteral>] Terminal =
        /// The name of the terminal.
        abstract name: string
        /// The process ID of the shell process.
        abstract processId: Thenable<float>
        /// <summary>Send text to the terminal. The text is written to the stdin of the underlying pty process
        /// (shell) of the terminal.</summary>
        /// <param name="text">The text to send.</param>
        /// <param name="addNewLine">Whether to add a new line to the text being sent, this is normally
        /// required to run a command in the terminal. The character(s) added are \n or \r\n
        /// depending on the platform. This defaults to `true`.</param>
        abstract sendText: text: string * ?addNewLine: bool -> unit
        /// <summary>Show the terminal panel and reveal this terminal in the UI.</summary>
        /// <param name="preserveFocus">When `true` the terminal will not take focus.</param>
        abstract show: ?preserveFocus: bool -> unit
        /// Hide the terminal panel if this terminal is currently showing.
        abstract hide: unit -> unit
        /// Dispose and free associated resources.
        abstract dispose: unit -> unit

    /// Represents an extension.
    /// 
    /// To get an instance of an `Extension` use [getExtension](#extensions.getExtension).
    type [<AllowNullLiteral>] Extension<'T> =
        /// The canonical extension identifier in the form of: `publisher.name`.
        abstract id: string
        /// The absolute file path of the directory containing this extension.
        abstract extensionPath: string
        /// `true` if the extension has been activated.
        abstract isActive: bool
        /// The parsed contents of the extension's package.json.
        abstract packageJSON: obj option
        /// The public API exported by this extension. It is an invalid action
        /// to access this field before this extension has been activated.
        abstract exports: 'T
        /// Activates this extension and returns its public API.
        abstract activate: unit -> Thenable<'T>

    /// An extension context is a collection of utilities private to an
    /// extension.
    /// 
    /// An instance of an `ExtensionContext` is provided as the first
    /// parameter to the `activate`-call of an extension.
    type [<AllowNullLiteral>] ExtensionContext =
        /// An array to which disposables can be added. When this
        /// extension is deactivated the disposables will be disposed.
        abstract subscriptions: ResizeArray<obj> with get, set
        /// A memento object that stores state in the context
        /// of the currently opened [workspace](#workspace.workspaceFolders).
        abstract workspaceState: Memento with get, set
        /// A memento object that stores state independent
        /// of the current opened [workspace](#workspace.workspaceFolders).
        abstract globalState: Memento with get, set
        /// The absolute file path of the directory containing the extension.
        abstract extensionPath: string with get, set
        /// <summary>Get the absolute path of a resource contained in the extension.</summary>
        /// <param name="relativePath">A relative path to a resource contained in the extension.</param>
        abstract asAbsolutePath: relativePath: string -> string
        /// An absolute file path of a workspace specific directory in which the extension
        /// can store private state. The directory might not exist on disk and creation is
        /// up to the extension. However, the parent directory is guaranteed to be existent.
        /// 
        /// Use [`workspaceState`](#ExtensionContext.workspaceState) or
        /// [`globalState`](#ExtensionContext.globalState) to store key value data.
        abstract storagePath: string option with get, set

    /// A memento represents a storage utility. It can store and retrieve
    /// values.
    type [<AllowNullLiteral>] Memento =
        /// <summary>Return a value.</summary>
        /// <param name="key">A string.</param>
        abstract get: key: string -> 'T option
        /// <summary>Return a value.</summary>
        /// <param name="key">A string.</param>
        /// <param name="defaultValue">A value that should be returned when there is no
        /// value (`undefined`) with the given key.</param>
        abstract get: key: string * defaultValue: 'T -> 'T
        /// <summary>Store a value. The value must be JSON-stringifyable.</summary>
        /// <param name="key">A string.</param>
        /// <param name="value">A value. MUST not contain cyclic references.</param>
        abstract update: key: string * value: obj option -> Thenable<unit>

    type [<RequireQualifiedAccess>] TaskRevealKind =
        | Always = 1
        | Silent = 2
        | Never = 3

    type [<RequireQualifiedAccess>] TaskPanelKind =
        | Shared = 1
        | Dedicated = 2
        | New = 3

    /// Controls how the task is presented in the UI.
    type [<AllowNullLiteral>] TaskPresentationOptions =
        /// Controls whether the task output is reveal in the user interface.
        /// Defaults to `RevealKind.Always`.
        abstract reveal: TaskRevealKind option with get, set
        /// Controls whether the command associated with the task is echoed
        /// in the user interface.
        abstract echo: bool option with get, set
        /// Controls whether the panel showing the task output is taking focus.
        abstract focus: bool option with get, set
        /// Controls if the task panel is used for this task only (dedicated),
        /// shared between tasks (shared) or if a new panel is created on
        /// every task execution (new). Defaults to `TaskInstanceKind.Shared`
        abstract panel: TaskPanelKind option with get, set

    /// A grouping for tasks. The editor by default supports the
    /// 'Clean', 'Build', 'RebuildAll' and 'Test' group.
    type [<AllowNullLiteral>] TaskGroup =
        /// The clean task group;
        abstract Clean: TaskGroup with get, set
        /// The build task group;
        abstract Build: TaskGroup with get, set
        /// The rebuild all task group;
        abstract Rebuild: TaskGroup with get, set
        /// The test all task group;
        abstract Test: TaskGroup with get, set

    /// A grouping for tasks. The editor by default supports the
    /// 'Clean', 'Build', 'RebuildAll' and 'Test' group.
    type [<AllowNullLiteral>] TaskGroupStatic =
        [<Emit "new $0($1...)">] abstract Create: id: string * label: string -> TaskGroup

    /// A structure that defines a task kind in the system.
    /// The value must be JSON-stringifyable.
    type [<AllowNullLiteral>] TaskDefinition =
        /// The task definition describing the task provided by an extension.
        /// Usually a task provider defines more properties to identify
        /// a task. They need to be defined in the package.json of the
        /// extension under the 'taskDefinitions' extension point. The npm
        /// task definition for example looks like this
        /// ```typescript
        /// interface NpmTaskDefinition extends TaskDefinition {
        ///      script: string;
        /// }
        /// ```
        abstract ``type``: string
        /// Additional attributes of a concrete task definition.
        [<Emit "$0[$1]{{=$2}}">] abstract Item: name: string -> obj option with get, set

    /// Options for a process execution
    type [<AllowNullLiteral>] ProcessExecutionOptions =
        /// The current working directory of the executed program or shell.
        /// If omitted the tools current workspace root is used.
        abstract cwd: string option with get, set
        /// The additional environment of the executed program or shell. If omitted
        /// the parent process' environment is used. If provided it is merged with
        /// the parent process' environment.
        abstract env: obj option with get, set

    /// The execution of a task happens as an external process
    /// without shell interaction.
    type [<AllowNullLiteral>] ProcessExecution =
        /// The process to be executed.
        abstract ``process``: string with get, set
        /// The arguments passed to the process. Defaults to an empty array.
        abstract args: ResizeArray<string> with get, set
        /// The process options used when the process is executed.
        /// Defaults to undefined.
        abstract options: ProcessExecutionOptions option with get, set

    /// The execution of a task happens as an external process
    /// without shell interaction.
    type [<AllowNullLiteral>] ProcessExecutionStatic =
        /// <summary>Creates a process execution.</summary>
        /// <param name="process">The process to start.</param>
        /// <param name="options">Optional options for the started process.</param>
        [<Emit "new $0($1...)">] abstract Create: ``process``: string * ?options: ProcessExecutionOptions -> ProcessExecution
        /// <summary>Creates a process execution.</summary>
        /// <param name="process">The process to start.</param>
        /// <param name="args">Arguments to be passed to the process.</param>
        /// <param name="options">Optional options for the started process.</param>
        [<Emit "new $0($1...)">] abstract Create: ``process``: string * args: ResizeArray<string> * ?options: ProcessExecutionOptions -> ProcessExecution

    /// Options for a shell execution
    type [<AllowNullLiteral>] ShellExecutionOptions =
        /// The shell executable.
        abstract executable: string option with get, set
        /// The arguments to be passed to the shell executable used to run the task.
        abstract shellArgs: ResizeArray<string> option with get, set
        /// The current working directory of the executed shell.
        /// If omitted the tools current workspace root is used.
        abstract cwd: string option with get, set
        /// The additional environment of the executed shell. If omitted
        /// the parent process' environment is used. If provided it is merged with
        /// the parent process' environment.
        abstract env: obj option with get, set

    type [<AllowNullLiteral>] ShellExecution =
        /// The shell command line
        abstract commandLine: string with get, set
        /// The shell options used when the command line is executed in a shell.
        /// Defaults to undefined.
        abstract options: ShellExecutionOptions option with get, set

    type [<AllowNullLiteral>] ShellExecutionStatic =
        /// <summary>Creates a process execution.</summary>
        /// <param name="commandLine">The command line to execute.</param>
        /// <param name="options">Optional options for the started the shell.</param>
        [<Emit "new $0($1...)">] abstract Create: commandLine: string * ?options: ShellExecutionOptions -> ShellExecution

    type [<RequireQualifiedAccess>] TaskScope =
        | Global = 1
        | Workspace = 2

    /// A task to execute
    type [<AllowNullLiteral>] Task =
        /// The task's definition.
        abstract definition: TaskDefinition with get, set
        /// The task's scope.
        abstract scope: U2<TaskScope, WorkspaceFolder> option with get, set
        /// The task's name
        abstract name: string with get, set
        /// The task's execution engine
        abstract execution: U2<ProcessExecution, ShellExecution> with get, set
        /// Whether the task is a background task or not.
        abstract isBackground: bool with get, set
        /// A human-readable string describing the source of this
        /// shell task, e.g. 'gulp' or 'npm'.
        abstract source: string with get, set
        /// The task group this tasks belongs to. See TaskGroup
        /// for a predefined set of available groups.
        /// Defaults to undefined meaning that the task doesn't
        /// belong to any special group.
        abstract group: TaskGroup option with get, set
        /// The presentation options. Defaults to an empty literal.
        abstract presentationOptions: TaskPresentationOptions with get, set
        /// The problem matchers attached to the task. Defaults to an empty
        /// array.
        abstract problemMatchers: ResizeArray<string> with get, set

    /// A task to execute
    type [<AllowNullLiteral>] TaskStatic =
        /// <summary>~~Creates a new task.~~</summary>
        /// <param name="name">The task's name. Is presented in the user interface.</param>
        /// <param name="source">The task's source (e.g. 'gulp', 'npm', ...). Is presented in the user interface.</param>
        /// <param name="execution">The process or shell execution.</param>
        /// <param name="problemMatchers">the names of problem matchers to use, like '$tsc'
        /// or '$eslint'. Problem matchers can be contributed by an extension using
        /// the `problemMatchers` extension point.</param>
        [<Emit "new $0($1...)">] abstract Create: taskDefinition: TaskDefinition * name: string * source: string * ?execution: U2<ProcessExecution, ShellExecution> * ?problemMatchers: U2<string, ResizeArray<string>> -> Task
        /// <summary>Creates a new task.</summary>
        /// <param name="target">Specifies the task's target. It is either a global or a workspace task or a task for a specific workspace folder.</param>
        /// <param name="name">The task's name. Is presented in the user interface.</param>
        /// <param name="source">The task's source (e.g. 'gulp', 'npm', ...). Is presented in the user interface.</param>
        /// <param name="execution">The process or shell execution.</param>
        /// <param name="problemMatchers">the names of problem matchers to use, like '$tsc'
        /// or '$eslint'. Problem matchers can be contributed by an extension using
        /// the `problemMatchers` extension point.</param>
        [<Emit "new $0($1...)">] abstract Create: taskDefinition: TaskDefinition * target: U2<WorkspaceFolder, TaskScope> * name: string * source: string * ?execution: U2<ProcessExecution, ShellExecution> * ?problemMatchers: U2<string, ResizeArray<string>> -> Task

    /// A task provider allows to add tasks to the task service.
    /// A task provider is registered via #workspace.registerTaskProvider.
    type [<AllowNullLiteral>] TaskProvider =
        /// <summary>Provides tasks.</summary>
        /// <param name="token">A cancellation token.</param>
        abstract provideTasks: ?token: CancellationToken -> ProviderResult<ResizeArray<Task>>
        /// <summary>Resolves a task that has no [`execution`](#Task.execution) set. Tasks are
        /// often created from information found in the `task.json`-file. Such tasks miss
        /// the information on how to execute them and a task provider must fill in
        /// the missing information in the `resolveTask`-method.</summary>
        /// <param name="task">The task to resolve.</param>
        /// <param name="token">A cancellation token.</param>
        abstract resolveTask: task: Task * ?token: CancellationToken -> ProviderResult<Task>

    module Env =

        type [<AllowNullLiteral>] IExports =
            abstract appName: string
            abstract appRoot: string
            abstract language: string
            abstract machineId: string
            abstract sessionId: string

    module Commands =

        type [<AllowNullLiteral>] IExports =
            /// <summary>Registers a command that can be invoked via a keyboard shortcut,
            /// a menu item, an action, or directly.
            /// 
            /// Registering a command with an existing command identifier twice
            /// will cause an error.</summary>
            /// <param name="command">A unique identifier for the command.</param>
            /// <param name="callback">A command handler function.</param>
            /// <param name="thisArg">The `this` context used when invoking the handler function.</param>
            abstract registerCommand: command: string * callback: (ResizeArray<obj option> -> obj option) * ?thisArg: obj option -> Disposable
            /// <summary>Registers a text editor command that can be invoked via a keyboard shortcut,
            /// a menu item, an action, or directly.
            /// 
            /// Text editor commands are different from ordinary [commands](#commands.registerCommand) as
            /// they only execute when there is an active editor when the command is called. Also, the
            /// command handler of an editor command has access to the active editor and to an
            /// [edit](#TextEditorEdit)-builder.</summary>
            /// <param name="command">A unique identifier for the command.</param>
            /// <param name="callback">A command handler function with access to an [editor](#TextEditor) and an [edit](#TextEditorEdit).</param>
            /// <param name="thisArg">The `this` context used when invoking the handler function.</param>
            abstract registerTextEditorCommand: command: string * callback: (TextEditor -> TextEditorEdit -> ResizeArray<obj option> -> unit) * ?thisArg: obj option -> Disposable
            /// <summary>Executes the command denoted by the given command identifier.
            /// 
            /// * *Note 1:* When executing an editor command not all types are allowed to
            /// be passed as arguments. Allowed are the primitive types `string`, `boolean`,
            /// `number`, `undefined`, and `null`, as well as [`Position`](#Position), [`Range`](#Range), [`Uri`](#Uri) and [`Location`](#Location).
            /// * *Note 2:* There are no restrictions when executing commands that have been contributed
            /// by extensions.</summary>
            /// <param name="command">Identifier of the command to execute.</param>
            /// <param name="rest">Parameters passed to the command function.</param>
            abstract executeCommand: command: string * [<ParamArray>] rest: ResizeArray<obj option> -> Thenable<'T option>
            /// <summary>Retrieve the list of all available commands. Commands starting an underscore are
            /// treated as internal commands.</summary>
            /// <param name="filterInternal">Set `true` to not see internal commands (starting with an underscore)</param>
            abstract getCommands: ?filterInternal: bool -> Thenable<ResizeArray<string>>

    /// Represents the state of a window.
    type [<AllowNullLiteral>] WindowState =
        /// Whether the current window is focused.
        abstract focused: bool

    module Window =

        type [<AllowNullLiteral>] IExports =
            abstract activeTextEditor: TextEditor option
            abstract visibleTextEditors: ResizeArray<TextEditor>
            abstract onDidChangeActiveTextEditor: Event<TextEditor>
            abstract onDidChangeVisibleTextEditors: Event<ResizeArray<TextEditor>>
            abstract onDidChangeTextEditorSelection: Event<TextEditorSelectionChangeEvent>
            abstract onDidChangeTextEditorOptions: Event<TextEditorOptionsChangeEvent>
            abstract onDidChangeTextEditorViewColumn: Event<TextEditorViewColumnChangeEvent>
            abstract onDidCloseTerminal: Event<Terminal>
            abstract state: WindowState
            abstract onDidChangeWindowState: Event<WindowState>
            /// <summary>Show the given document in a text editor. A [column](#ViewColumn) can be provided
            /// to control where the editor is being shown. Might change the [active editor](#window.activeTextEditor).</summary>
            /// <param name="document">A text document to be shown.</param>
            /// <param name="column">A view column in which the [editor](#TextEditor) should be shown. The default is the [one](#ViewColumn.One), other values
            /// are adjusted to be `Min(column, columnCount + 1)`, the [active](#ViewColumn.Active)-column is
            /// not adjusted.</param>
            /// <param name="preserveFocus">When `true` the editor will not take focus.</param>
            abstract showTextDocument: document: TextDocument * ?column: ViewColumn * ?preserveFocus: bool -> Thenable<TextEditor>
            /// <summary>Show the given document in a text editor. [Options](#TextDocumentShowOptions) can be provided
            /// to control options of the editor is being shown. Might change the [active editor](#window.activeTextEditor).</summary>
            /// <param name="document">A text document to be shown.</param>
            /// <param name="options">[Editor options](#TextDocumentShowOptions) to configure the behavior of showing the [editor](#TextEditor).</param>
            abstract showTextDocument: document: TextDocument * ?options: TextDocumentShowOptions -> Thenable<TextEditor>
            /// <summary>A short-hand for `openTextDocument(uri).then(document => showTextDocument(document, options))`.</summary>
            /// <param name="uri">A resource identifier.</param>
            /// <param name="options">[Editor options](#TextDocumentShowOptions) to configure the behavior of showing the [editor](#TextEditor).</param>
            abstract showTextDocument: uri: Uri * ?options: TextDocumentShowOptions -> Thenable<TextEditor>
            /// <summary>Create a TextEditorDecorationType that can be used to add decorations to text editors.</summary>
            /// <param name="options">Rendering options for the decoration type.</param>
            abstract createTextEditorDecorationType: options: DecorationRenderOptions -> TextEditorDecorationType
            /// <summary>Show an information message to users. Optionally provide an array of items which will be presented as
            /// clickable buttons.</summary>
            /// <param name="message">The message to show.</param>
            /// <param name="items">A set of items that will be rendered as actions in the message.</param>
            abstract showInformationMessage: message: string * [<ParamArray>] items: ResizeArray<string> -> Thenable<string option>
            /// <summary>Show an information message to users. Optionally provide an array of items which will be presented as
            /// clickable buttons.</summary>
            /// <param name="message">The message to show.</param>
            /// <param name="options">Configures the behaviour of the message.</param>
            /// <param name="items">A set of items that will be rendered as actions in the message.</param>
            abstract showInformationMessage: message: string * options: MessageOptions * [<ParamArray>] items: ResizeArray<string> -> Thenable<string option>
            /// <summary>Show an information message.</summary>
            /// <param name="message">The message to show.</param>
            /// <param name="items">A set of items that will be rendered as actions in the message.</param>
            abstract showInformationMessage: message: string * [<ParamArray>] items: ResizeArray<'T> -> Thenable<'T option>
            /// <summary>Show an information message.</summary>
            /// <param name="message">The message to show.</param>
            /// <param name="options">Configures the behaviour of the message.</param>
            /// <param name="items">A set of items that will be rendered as actions in the message.</param>
            abstract showInformationMessage: message: string * options: MessageOptions * [<ParamArray>] items: ResizeArray<'T> -> Thenable<'T option>
            /// <summary>Show a warning message.</summary>
            /// <param name="message">The message to show.</param>
            /// <param name="items">A set of items that will be rendered as actions in the message.</param>
            abstract showWarningMessage: message: string * [<ParamArray>] items: ResizeArray<string> -> Thenable<string option>
            /// <summary>Show a warning message.</summary>
            /// <param name="message">The message to show.</param>
            /// <param name="options">Configures the behaviour of the message.</param>
            /// <param name="items">A set of items that will be rendered as actions in the message.</param>
            abstract showWarningMessage: message: string * options: MessageOptions * [<ParamArray>] items: ResizeArray<string> -> Thenable<string option>
            /// <summary>Show a warning message.</summary>
            /// <param name="message">The message to show.</param>
            /// <param name="items">A set of items that will be rendered as actions in the message.</param>
            abstract showWarningMessage: message: string * [<ParamArray>] items: ResizeArray<'T> -> Thenable<'T option>
            /// <summary>Show a warning message.</summary>
            /// <param name="message">The message to show.</param>
            /// <param name="options">Configures the behaviour of the message.</param>
            /// <param name="items">A set of items that will be rendered as actions in the message.</param>
            abstract showWarningMessage: message: string * options: MessageOptions * [<ParamArray>] items: ResizeArray<'T> -> Thenable<'T option>
            /// <summary>Show an error message.</summary>
            /// <param name="message">The message to show.</param>
            /// <param name="items">A set of items that will be rendered as actions in the message.</param>
            abstract showErrorMessage: message: string * [<ParamArray>] items: ResizeArray<string> -> Thenable<string option>
            /// <summary>Show an error message.</summary>
            /// <param name="message">The message to show.</param>
            /// <param name="options">Configures the behaviour of the message.</param>
            /// <param name="items">A set of items that will be rendered as actions in the message.</param>
            abstract showErrorMessage: message: string * options: MessageOptions * [<ParamArray>] items: ResizeArray<string> -> Thenable<string option>
            /// <summary>Show an error message.</summary>
            /// <param name="message">The message to show.</param>
            /// <param name="items">A set of items that will be rendered as actions in the message.</param>
            abstract showErrorMessage: message: string * [<ParamArray>] items: ResizeArray<'T> -> Thenable<'T option>
            /// <summary>Show an error message.</summary>
            /// <param name="message">The message to show.</param>
            /// <param name="options">Configures the behaviour of the message.</param>
            /// <param name="items">A set of items that will be rendered as actions in the message.</param>
            abstract showErrorMessage: message: string * options: MessageOptions * [<ParamArray>] items: ResizeArray<'T> -> Thenable<'T option>
            /// <summary>Shows a selection list.</summary>
            /// <param name="items">An array of strings, or a promise that resolves to an array of strings.</param>
            /// <param name="options">Configures the behavior of the selection list.</param>
            /// <param name="token">A token that can be used to signal cancellation.</param>
            abstract showQuickPick: items: U2<ResizeArray<string>, Thenable<ResizeArray<string>>> * ?options: QuickPickOptions * ?token: CancellationToken -> Thenable<string option>
            /// <summary>Shows a selection list.</summary>
            /// <param name="items">An array of items, or a promise that resolves to an array of items.</param>
            /// <param name="options">Configures the behavior of the selection list.</param>
            /// <param name="token">A token that can be used to signal cancellation.</param>
            abstract showQuickPick: items: U2<ResizeArray<'T>, Thenable<ResizeArray<'T>>> * ?options: QuickPickOptions * ?token: CancellationToken -> Thenable<'T option>
            /// <summary>Shows a selection list of [workspace folders](#workspace.workspaceFolders) to pick from.
            /// Returns `undefined` if no folder is open.</summary>
            /// <param name="options">Configures the behavior of the workspace folder list.</param>
            abstract showWorkspaceFolderPick: ?options: WorkspaceFolderPickOptions -> Thenable<WorkspaceFolder option>
            /// <summary>Shows a file open dialog to the user which allows to select a file
            /// for opening-purposes.</summary>
            /// <param name="options">Options that control the dialog.</param>
            abstract showOpenDialog: options: OpenDialogOptions -> Thenable<ResizeArray<Uri> option>
            /// <summary>Shows a file save dialog to the user which allows to select a file
            /// for saving-purposes.</summary>
            /// <param name="options">Options that control the dialog.</param>
            abstract showSaveDialog: options: SaveDialogOptions -> Thenable<Uri option>
            /// <summary>Opens an input box to ask the user for input.
            /// 
            /// The returned value will be `undefined` if the input box was canceled (e.g. pressing ESC). Otherwise the
            /// returned value will be the string typed by the user or an empty string if the user did not type
            /// anything but dismissed the input box with OK.</summary>
            /// <param name="options">Configures the behavior of the input box.</param>
            /// <param name="token">A token that can be used to signal cancellation.</param>
            abstract showInputBox: ?options: InputBoxOptions * ?token: CancellationToken -> Thenable<string option>
            /// <summary>Create a new [output channel](#OutputChannel) with the given name.</summary>
            /// <param name="name">Human-readable string which will be used to represent the channel in the UI.</param>
            abstract createOutputChannel: name: string -> OutputChannel
            /// <summary>Set a message to the status bar. This is a short hand for the more powerful
            /// status bar [items](#window.createStatusBarItem).</summary>
            /// <param name="text">The message to show, supports icon substitution as in status bar [items](#StatusBarItem.text).</param>
            /// <param name="hideAfterTimeout">Timeout in milliseconds after which the message will be disposed.</param>
            abstract setStatusBarMessage: text: string * hideAfterTimeout: float -> Disposable
            /// <summary>Set a message to the status bar. This is a short hand for the more powerful
            /// status bar [items](#window.createStatusBarItem).</summary>
            /// <param name="text">The message to show, supports icon substitution as in status bar [items](#StatusBarItem.text).</param>
            /// <param name="hideWhenDone">Thenable on which completion (resolve or reject) the message will be disposed.</param>
            abstract setStatusBarMessage: text: string * hideWhenDone: Thenable<obj option> -> Disposable
            /// <summary>Set a message to the status bar. This is a short hand for the more powerful
            /// status bar [items](#window.createStatusBarItem).
            /// 
            /// *Note* that status bar messages stack and that they must be disposed when no
            /// longer used.</summary>
            /// <param name="text">The message to show, supports icon substitution as in status bar [items](#StatusBarItem.text).</param>
            abstract setStatusBarMessage: text: string -> Disposable
            /// <summary>~~Show progress in the Source Control viewlet while running the given callback and while
            /// its returned promise isn't resolve or rejected.~~</summary>
            /// <param name="task">A callback returning a promise. Progress increments can be reported with
            /// the provided [progress](#Progress)-object.</param>
            abstract withScmProgress: task: (Progress<float> -> Thenable<'R>) -> Thenable<'R>
            /// <summary>Show progress in the editor. Progress is shown while running the given callback
            /// and while the promise it returned isn't resolved nor rejected. The location at which
            /// progress should show (and other details) is defined via the passed [`ProgressOptions`](#ProgressOptions).</summary>
            /// <param name="task">A callback returning a promise. Progress state can be reported with
            /// the provided [progress](#Progress)-object.</param>
            abstract withProgress: options: ProgressOptions * task: (Progress<obj> -> Thenable<'R>) -> Thenable<'R>
            /// <summary>Creates a status bar [item](#StatusBarItem).</summary>
            /// <param name="alignment">The alignment of the item.</param>
            /// <param name="priority">The priority of the item. Higher values mean the item should be shown more to the left.</param>
            abstract createStatusBarItem: ?alignment: StatusBarAlignment * ?priority: float -> StatusBarItem
            /// <summary>Creates a [Terminal](#Terminal). The cwd of the terminal will be the workspace directory
            /// if it exists, regardless of whether an explicit customStartPath setting exists.</summary>
            /// <param name="name">Optional human-readable string which will be used to represent the terminal in the UI.</param>
            /// <param name="shellPath">Optional path to a custom shell executable to be used in the terminal.</param>
            /// <param name="shellArgs">Optional args for the custom shell executable, this does not work on Windows (see #8429)</param>
            abstract createTerminal: ?name: string * ?shellPath: string * ?shellArgs: ResizeArray<string> -> Terminal
            /// <summary>Creates a [Terminal](#Terminal). The cwd of the terminal will be the workspace directory
            /// if it exists, regardless of whether an explicit customStartPath setting exists.</summary>
            /// <param name="options">A TerminalOptions object describing the characteristics of the new terminal.</param>
            abstract createTerminal: options: TerminalOptions -> Terminal
            /// <summary>Register a [TreeDataProvider](#TreeDataProvider) for the view contributed using the extension point `views`.</summary>
            /// <param name="viewId">Id of the view contributed using the extension point `views`.</param>
            /// <param name="treeDataProvider">A [TreeDataProvider](#TreeDataProvider) that provides tree data for the view</param>
            abstract registerTreeDataProvider: viewId: string * treeDataProvider: TreeDataProvider<'T> -> Disposable

    /// A data provider that provides tree data
    type [<AllowNullLiteral>] TreeDataProvider<'T> =
        /// An optional event to signal that an element or root has changed.
        /// To signal that root has changed, do not pass any argument or pass `undefined` or `null`.
        abstract onDidChangeTreeData: Event<'T option> option with get, set
        /// <summary>Get [TreeItem](#TreeItem) representation of the `element`</summary>
        /// <param name="element">The element for which [TreeItem](#TreeItem) representation is asked for.</param>
        abstract getTreeItem: element: 'T -> U2<TreeItem, Thenable<TreeItem>>
        /// <summary>Get the children of `element` or root if no element is passed.</summary>
        /// <param name="element">The element from which the provider gets children. Can be `undefined`.</param>
        abstract getChildren: ?element: 'T -> ProviderResult<ResizeArray<'T>>

    type [<AllowNullLiteral>] TreeItem =
        /// A human-readable string describing this item
        abstract label: string with get, set
        /// The icon path for the tree item
        abstract iconPath: U3<string, Uri, obj> option with get, set
        /// The [command](#Command) which should be run when the tree item is selected.
        abstract command: Command option with get, set
        /// [TreeItemCollapsibleState](#TreeItemCollapsibleState) of the tree item.
        abstract collapsibleState: TreeItemCollapsibleState option with get, set
        /// Context value of the tree item. This can be used to contribute item specific actions in the tree.
        /// For example, a tree item is given a context value as `folder`. When contributing actions to `view/item/context`
        /// using `menus` extension point, you can specify context value for key `viewItem` in `when` expression like `viewItem == folder`.
        /// ```
        /// "contributes": {
        /// 		"menus": {
        /// 			"view/item/context": [
        /// 				{
        /// 					"command": "extension.deleteFolder",
        /// 					"when": "viewItem == folder"
        /// 				}
        /// 			]
        /// 		}
        /// }
        /// ```
        /// This will show action `extension.deleteFolder` only for items with `contextValue` is `folder`.
        abstract contextValue: string option with get, set

    type [<AllowNullLiteral>] TreeItemStatic =
        /// <param name="label">A human-readable string describing this item</param>
        /// <param name="collapsibleState">[TreeItemCollapsibleState](#TreeItemCollapsibleState) of the tree item. Default is [TreeItemCollapsibleState.None](#TreeItemCollapsibleState.None)</param>
        [<Emit "new $0($1...)">] abstract Create: label: string * ?collapsibleState: TreeItemCollapsibleState -> TreeItem

    type [<RequireQualifiedAccess>] TreeItemCollapsibleState =
        | None = 0
        | Collapsed = 1
        | Expanded = 2

    /// Value-object describing what options a terminal should use.
    type [<AllowNullLiteral>] TerminalOptions =
        /// A human-readable string which will be used to represent the terminal in the UI.
        abstract name: string option with get, set
        /// A path to a custom shell executable to be used in the terminal.
        abstract shellPath: string option with get, set
        /// Args for the custom shell executable, this does not work on Windows (see #8429)
        abstract shellArgs: ResizeArray<string> option with get, set
        /// Object with environment variables that will be added to the VS Code process.
        abstract env: obj option with get, set

    type [<RequireQualifiedAccess>] ProgressLocation =
        | SourceControl = 1
        | Window = 10

    /// Value-object describing where and how progress should show.
    type [<AllowNullLiteral>] ProgressOptions =
        /// The location at which progress should show.
        abstract location: ProgressLocation with get, set
        /// A human-readable string which will be used to describe the
        /// operation.
        abstract title: string option with get, set

    /// An event describing an individual change in the text of a [document](#TextDocument).
    type [<AllowNullLiteral>] TextDocumentContentChangeEvent =
        /// The range that got replaced.
        abstract range: Range with get, set
        /// The length of the range that got replaced.
        abstract rangeLength: float with get, set
        /// The new text for the range.
        abstract text: string with get, set

    /// An event describing a transactional [document](#TextDocument) change.
    type [<AllowNullLiteral>] TextDocumentChangeEvent =
        /// The affected document.
        abstract document: TextDocument with get, set
        /// An array of content changes.
        abstract contentChanges: ResizeArray<TextDocumentContentChangeEvent> with get, set

    type [<RequireQualifiedAccess>] TextDocumentSaveReason =
        | Manual = 1
        | AfterDelay = 2
        | FocusOut = 3

    /// An event that is fired when a [document](#TextDocument) will be saved.
    /// 
    /// To make modifications to the document before it is being saved, call the
    /// [`waitUntil`](#TextDocumentWillSaveEvent.waitUntil)-function with a thenable
    /// that resolves to an array of [text edits](#TextEdit).
    type [<AllowNullLiteral>] TextDocumentWillSaveEvent =
        /// The document that will be saved.
        abstract document: TextDocument with get, set
        /// The reason why save was triggered.
        abstract reason: TextDocumentSaveReason with get, set
        /// <summary>Allows to pause the event loop and to apply [pre-save-edits](#TextEdit).
        /// Edits of subsequent calls to this function will be applied in order. The
        /// edits will be *ignored* if concurrent modifications of the document happened.
        /// 
        /// *Note:* This function can only be called during event dispatch and not
        /// in an asynchronous manner:
        /// 
        /// ```ts
        /// workspace.onWillSaveTextDocument(event => {
        ///  	// async, will *throw* an error
        ///  	setTimeout(() => event.waitUntil(promise));
        /// 
        ///  	// sync, OK
        ///  	event.waitUntil(promise);
        /// })
        /// ```</summary>
        /// <param name="thenable">A thenable that resolves to [pre-save-edits](#TextEdit).</param>
        abstract waitUntil: thenable: Thenable<ResizeArray<TextEdit>> -> unit
        /// <summary>Allows to pause the event loop until the provided thenable resolved.
        /// 
        /// *Note:* This function can only be called during event dispatch.</summary>
        /// <param name="thenable">A thenable that delays saving.</param>
        abstract waitUntil: thenable: Thenable<obj option> -> unit

    /// An event describing a change to the set of [workspace folders](#workspace.workspaceFolders).
    type [<AllowNullLiteral>] WorkspaceFoldersChangeEvent =
        /// Added workspace folders.
        abstract added: ResizeArray<WorkspaceFolder>
        /// Removed workspace folders.
        abstract removed: ResizeArray<WorkspaceFolder>

    /// A workspace folder is one of potentially many roots opened by the editor. All workspace folders
    /// are equal which means there is no notion of an active or master workspace folder.
    type [<AllowNullLiteral>] WorkspaceFolder =
        /// The associated uri for this workspace folder.
        /// 
        /// *Note:* The [Uri](#Uri)-type was intentionally chosen such that future releases of the editor can support
        /// workspace folders that are not stored on the local disk, e.g. `ftp://server/workspaces/foo`.
        abstract uri: Uri
        /// The name of this workspace folder. Defaults to
        /// the basename of its [uri-path](#Uri.path)
        abstract name: string
        /// The ordinal number of this workspace folder.
        abstract index: float

    module Workspace =

        type [<AllowNullLiteral>] IExports =
            abstract rootPath: string option
            abstract workspaceFolders: ResizeArray<WorkspaceFolder> option
            abstract name: string option
            abstract onDidChangeWorkspaceFolders: Event<WorkspaceFoldersChangeEvent>
            /// <summary>Returns the [workspace folder](#WorkspaceFolder) that contains a given uri.
            /// * returns `undefined` when the given uri doesn't match any workspace folder
            /// * returns the *input* when the given uri is a workspace folder itself</summary>
            /// <param name="uri">An uri.</param>
            abstract getWorkspaceFolder: uri: Uri -> WorkspaceFolder option
            /// <summary>Returns a path that is relative to the workspace folder or folders.
            /// 
            /// When there are no [workspace folders](#workspace.workspaceFolders) or when the path
            /// is not contained in them, the input is returned.</summary>
            /// <param name="pathOrUri">A path or uri. When a uri is given its [fsPath](#Uri.fsPath) is used.</param>
            /// <param name="includeWorkspaceFolder">When `true` and when the given path is contained inside a
            /// workspace folder the name of the workspace is prepended. Defaults to `true` when there are
            /// multiple workspace folders and `false` otherwise.</param>
            abstract asRelativePath: pathOrUri: U2<string, Uri> * ?includeWorkspaceFolder: bool -> string
            /// <summary>Creates a file system watcher.
            /// 
            /// A glob pattern that filters the file events on their absolute path must be provided. Optionally,
            /// flags to ignore certain kinds of events can be provided. To stop listening to events the watcher must be disposed.
            /// 
            /// *Note* that only files within the current [workspace folders](#workspace.workspaceFolders) can be watched.</summary>
            /// <param name="globPattern">A [glob pattern](#GlobPattern) that is applied to the absolute paths of created, changed,
            /// and deleted files. Use a [relative pattern](#RelativePattern) to limit events to a certain [workspace folder](#WorkspaceFolder).</param>
            /// <param name="ignoreCreateEvents">Ignore when files have been created.</param>
            /// <param name="ignoreChangeEvents">Ignore when files have been changed.</param>
            /// <param name="ignoreDeleteEvents">Ignore when files have been deleted.</param>
            abstract createFileSystemWatcher: globPattern: GlobPattern * ?ignoreCreateEvents: bool * ?ignoreChangeEvents: bool * ?ignoreDeleteEvents: bool -> FileSystemWatcher
            /// <summary>Find files across all [workspace folders](#workspace.workspaceFolders) in the workspace.</summary>
            /// <param name="include">A [glob pattern](#GlobPattern) that defines the files to search for. The glob pattern
            /// will be matched against the file paths of resulting matches relative to their workspace. Use a [relative pattern](#RelativePattern)
            /// to restrict the search results to a [workspace folder](#WorkspaceFolder).</param>
            /// <param name="exclude">A [glob pattern](#GlobPattern) that defines files and folders to exclude. The glob pattern
            /// will be matched against the file paths of resulting matches relative to their workspace.</param>
            /// <param name="maxResults">An upper-bound for the result.</param>
            /// <param name="token">A token that can be used to signal cancellation to the underlying search engine.</param>
            abstract findFiles: ``include``: GlobPattern * ?exclude: GlobPattern * ?maxResults: float * ?token: CancellationToken -> Thenable<ResizeArray<Uri>>
            /// <summary>Save all dirty files.</summary>
            /// <param name="includeUntitled">Also save files that have been created during this session.</param>
            abstract saveAll: ?includeUntitled: bool -> Thenable<bool>
            /// <summary>Make changes to one or many resources as defined by the given
            /// [workspace edit](#WorkspaceEdit).
            /// 
            /// When applying a workspace edit, the editor implements an 'all-or-nothing'-strategy,
            /// that means failure to load one document or make changes to one document will cause
            /// the edit to be rejected.</summary>
            /// <param name="edit">A workspace edit.</param>
            abstract applyEdit: edit: WorkspaceEdit -> Thenable<bool>
            abstract textDocuments: ResizeArray<TextDocument>
            /// <summary>Opens a document. Will return early if this document is already open. Otherwise
            /// the document is loaded and the [didOpen](#workspace.onDidOpenTextDocument)-event fires.
            /// 
            /// The document is denoted by an [uri](#Uri). Depending on the [scheme](#Uri.scheme) the
            /// following rules apply:
            /// * `file`-scheme: Open a file on disk, will be rejected if the file does not exist or cannot be loaded.
            /// * `untitled`-scheme: A new file that should be saved on disk, e.g. `untitled:c:\frodo\new.js`. The language
            /// will be derived from the file name.
            /// * For all other schemes the registered text document content [providers](#TextDocumentContentProvider) are consulted.
            /// 
            /// *Note* that the lifecycle of the returned document is owned by the editor and not by the extension. That means an
            /// [`onDidClose`](#workspace.onDidCloseTextDocument)-event can occur at any time after opening it.</summary>
            /// <param name="uri">Identifies the resource to open.</param>
            abstract openTextDocument: uri: Uri -> Thenable<TextDocument>
            /// <summary>A short-hand for `openTextDocument(Uri.file(fileName))`.</summary>
            /// <param name="fileName">A name of a file on disk.</param>
            abstract openTextDocument: fileName: string -> Thenable<TextDocument>
            /// <summary>Opens an untitled text document. The editor will prompt the user for a file
            /// path when the document is to be saved. The `options` parameter allows to
            /// specify the *language* and/or the *content* of the document.</summary>
            /// <param name="options">Options to control how the document will be created.</param>
            abstract openTextDocument: ?options: OpenTextDocumentOptions -> Thenable<TextDocument>
            /// <summary>Register a text document content provider.
            /// 
            /// Only one provider can be registered per scheme.</summary>
            /// <param name="scheme">The uri-scheme to register for.</param>
            /// <param name="provider">A content provider.</param>
            abstract registerTextDocumentContentProvider: scheme: string * provider: TextDocumentContentProvider -> Disposable
            abstract onDidOpenTextDocument: Event<TextDocument>
            abstract onDidCloseTextDocument: Event<TextDocument>
            abstract onDidChangeTextDocument: Event<TextDocumentChangeEvent>
            abstract onWillSaveTextDocument: Event<TextDocumentWillSaveEvent>
            abstract onDidSaveTextDocument: Event<TextDocument>
            /// <summary>Get a workspace configuration object.
            /// 
            /// When a section-identifier is provided only that part of the configuration
            /// is returned. Dots in the section-identifier are interpreted as child-access,
            /// like `{ myExt: { setting: { doIt: true }}}` and `getConfiguration('myExt.setting').get('doIt') === true`.
            /// 
            /// When a resource is provided, configuration scoped to that resource is returned.</summary>
            /// <param name="section">A dot-separated identifier.</param>
            /// <param name="resource">A resource for which the configuration is asked for</param>
            abstract getConfiguration: ?section: string * ?resource: Uri -> WorkspaceConfiguration
            abstract onDidChangeConfiguration: Event<ConfigurationChangeEvent>
            /// <summary>Register a task provider.</summary>
            /// <param name="type">The task kind type this provider is registered for.</param>
            /// <param name="provider">A task provider.</param>
            abstract registerTaskProvider: ``type``: string * provider: TaskProvider -> Disposable

        type [<AllowNullLiteral>] OpenTextDocumentOptions =
            abstract language: string option with get, set
            abstract content: string option with get, set

    /// An event describing the change in Configuration
    type [<AllowNullLiteral>] ConfigurationChangeEvent =
        /// <summary>Returns `true` if the given section for the given resource (if provided) is affected.</summary>
        /// <param name="section">Configuration name, supports _dotted_ names.</param>
        /// <param name="resource">A resource Uri.</param>
        abstract affectsConfiguration: section: string * ?resource: Uri -> bool

    module Languages =

        type [<AllowNullLiteral>] IExports =
            /// Return the identifiers of all known languages.
            abstract getLanguages: unit -> Thenable<ResizeArray<string>>
            /// <summary>Compute the match between a document [selector](#DocumentSelector) and a document. Values
            /// greater than zero mean the selector matches the document.
            /// 
            /// A match is computed according to these rules:
            /// 1. When [`DocumentSelector`](#DocumentSelector) is an array, compute the match for each contained `DocumentFilter` or language identifier and take the maximum value.
            /// 2. A string will be desugared to become the `language`-part of a [`DocumentFilter`](#DocumentFilter), so `"fooLang"` is like `{ language: "fooLang" }`.
            /// 3. A [`DocumentFilter`](#DocumentFilter) will be matched against the document by comparing its parts with the document. The following rules apply:
            ///   1. When the `DocumentFilter` is empty (`{}`) the result is `0`
            ///   2. When `scheme`, `language`, or `pattern` are defined but one doesn’t match, the result is `0`
            ///   3. Matching against `*` gives a score of `5`, matching via equality or via a glob-pattern gives a score of `10`
            ///   4. The result is the maximun value of each match
            /// 
            /// Samples:
            /// ```js
            /// // default document from disk (file-scheme)
            /// doc.uri; //'file:///my/file.js'
            /// doc.languageId; // 'javascript'
            /// match('javascript', doc); // 10;
            /// match({language: 'javascript'}, doc); // 10;
            /// match({language: 'javascript', scheme: 'file'}, doc); // 10;
            /// match('*', doc); // 5
            /// match('fooLang', doc); // 0
            /// match(['fooLang', '*'], doc); // 5
            /// 
            /// // virtual document, e.g. from git-index
            /// doc.uri; // 'git:/my/file.js'
            /// doc.languageId; // 'javascript'
            /// match('javascript', doc); // 10;
            /// match({language: 'javascript', scheme: 'git'}, doc); // 10;
            /// match('*', doc); // 5
            /// ```</summary>
            /// <param name="selector">A document selector.</param>
            /// <param name="document">A text document.</param>
            abstract ``match``: selector: DocumentSelector * document: TextDocument -> float
            /// <summary>Create a diagnostics collection.</summary>
            /// <param name="name">The [name](#DiagnosticCollection.name) of the collection.</param>
            abstract createDiagnosticCollection: ?name: string -> DiagnosticCollection
            /// <summary>Register a completion provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are sorted
            /// by their [score](#languages.match) and groups of equal score are sequentially asked for
            /// completion items. The process stops when one or many providers of a group return a
            /// result. A failing provider (rejected promise or exception) will not fail the whole
            /// operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A completion provider.</param>
            /// <param name="triggerCharacters">Trigger completion when the user types one of the characters, like `.` or `:`.</param>
            abstract registerCompletionItemProvider: selector: DocumentSelector * provider: CompletionItemProvider * [<ParamArray>] triggerCharacters: ResizeArray<string> -> Disposable
            /// <summary>Register a code action provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are asked in
            /// parallel and the results are merged. A failing provider (rejected promise or exception) will
            /// not cause a failure of the whole operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A code action provider.</param>
            abstract registerCodeActionsProvider: selector: DocumentSelector * provider: CodeActionProvider -> Disposable
            /// <summary>Register a code lens provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are asked in
            /// parallel and the results are merged. A failing provider (rejected promise or exception) will
            /// not cause a failure of the whole operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A code lens provider.</param>
            abstract registerCodeLensProvider: selector: DocumentSelector * provider: CodeLensProvider -> Disposable
            /// <summary>Register a definition provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are asked in
            /// parallel and the results are merged. A failing provider (rejected promise or exception) will
            /// not cause a failure of the whole operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A definition provider.</param>
            abstract registerDefinitionProvider: selector: DocumentSelector * provider: DefinitionProvider -> Disposable
            /// <summary>Register an implementation provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are asked in
            /// parallel and the results are merged. A failing provider (rejected promise or exception) will
            /// not cause a failure of the whole operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">An implementation provider.</param>
            abstract registerImplementationProvider: selector: DocumentSelector * provider: ImplementationProvider -> Disposable
            /// <summary>Register a type definition provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are asked in
            /// parallel and the results are merged. A failing provider (rejected promise or exception) will
            /// not cause a failure of the whole operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A type definition provider.</param>
            abstract registerTypeDefinitionProvider: selector: DocumentSelector * provider: TypeDefinitionProvider -> Disposable
            /// <summary>Register a hover provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are asked in
            /// parallel and the results are merged. A failing provider (rejected promise or exception) will
            /// not cause a failure of the whole operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A hover provider.</param>
            abstract registerHoverProvider: selector: DocumentSelector * provider: HoverProvider -> Disposable
            /// <summary>Register a document highlight provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are sorted
            /// by their [score](#languages.match) and groups sequentially asked for document highlights.
            /// The process stops when a provider returns a `non-falsy` or `non-failure` result.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A document highlight provider.</param>
            abstract registerDocumentHighlightProvider: selector: DocumentSelector * provider: DocumentHighlightProvider -> Disposable
            /// <summary>Register a document symbol provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are asked in
            /// parallel and the results are merged. A failing provider (rejected promise or exception) will
            /// not cause a failure of the whole operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A document symbol provider.</param>
            abstract registerDocumentSymbolProvider: selector: DocumentSelector * provider: DocumentSymbolProvider -> Disposable
            /// <summary>Register a workspace symbol provider.
            /// 
            /// Multiple providers can be registered. In that case providers are asked in parallel and
            /// the results are merged. A failing provider (rejected promise or exception) will not cause
            /// a failure of the whole operation.</summary>
            /// <param name="provider">A workspace symbol provider.</param>
            abstract registerWorkspaceSymbolProvider: provider: WorkspaceSymbolProvider -> Disposable
            /// <summary>Register a reference provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are asked in
            /// parallel and the results are merged. A failing provider (rejected promise or exception) will
            /// not cause a failure of the whole operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A reference provider.</param>
            abstract registerReferenceProvider: selector: DocumentSelector * provider: ReferenceProvider -> Disposable
            /// <summary>Register a reference provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are sorted
            /// by their [score](#languages.match) and the best-matching provider is used. Failure
            /// of the selected provider will cause a failure of the whole operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A rename provider.</param>
            abstract registerRenameProvider: selector: DocumentSelector * provider: RenameProvider -> Disposable
            /// <summary>Register a formatting provider for a document.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are sorted
            /// by their [score](#languages.match) and the best-matching provider is used. Failure
            /// of the selected provider will cause a failure of the whole operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A document formatting edit provider.</param>
            abstract registerDocumentFormattingEditProvider: selector: DocumentSelector * provider: DocumentFormattingEditProvider -> Disposable
            /// <summary>Register a formatting provider for a document range.
            /// 
            /// *Note:* A document range provider is also a [document formatter](#DocumentFormattingEditProvider)
            /// which means there is no need to [register](registerDocumentFormattingEditProvider) a document
            /// formatter when also registering a range provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are sorted
            /// by their [score](#languages.match) and the best-matching provider is used. Failure
            /// of the selected provider will cause a failure of the whole operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A document range formatting edit provider.</param>
            abstract registerDocumentRangeFormattingEditProvider: selector: DocumentSelector * provider: DocumentRangeFormattingEditProvider -> Disposable
            /// <summary>Register a formatting provider that works on type. The provider is active when the user enables the setting `editor.formatOnType`.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are sorted
            /// by their [score](#languages.match) and the best-matching provider is used. Failure
            /// of the selected provider will cause a failure of the whole operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">An on type formatting edit provider.</param>
            /// <param name="firstTriggerCharacter">A character on which formatting should be triggered, like `}`.</param>
            /// <param name="moreTriggerCharacter">More trigger characters.</param>
            abstract registerOnTypeFormattingEditProvider: selector: DocumentSelector * provider: OnTypeFormattingEditProvider * firstTriggerCharacter: string * [<ParamArray>] moreTriggerCharacter: ResizeArray<string> -> Disposable
            /// <summary>Register a signature help provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are sorted
            /// by their [score](#languages.match) and called sequentially until a provider returns a
            /// valid result.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A signature help provider.</param>
            /// <param name="triggerCharacters">Trigger signature help when the user types one of the characters, like `,` or `(`.</param>
            abstract registerSignatureHelpProvider: selector: DocumentSelector * provider: SignatureHelpProvider * [<ParamArray>] triggerCharacters: ResizeArray<string> -> Disposable
            /// <summary>Register a document link provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are asked in
            /// parallel and the results are merged. A failing provider (rejected promise or exception) will
            /// not cause a failure of the whole operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A document link provider.</param>
            abstract registerDocumentLinkProvider: selector: DocumentSelector * provider: DocumentLinkProvider -> Disposable
            /// <summary>Register a color provider.
            /// 
            /// Multiple providers can be registered for a language. In that case providers are asked in
            /// parallel and the results are merged. A failing provider (rejected promise or exception) will
            /// not cause a failure of the whole operation.</summary>
            /// <param name="selector">A selector that defines the documents this provider is applicable to.</param>
            /// <param name="provider">A color provider.</param>
            abstract registerColorProvider: selector: DocumentSelector * provider: DocumentColorProvider -> Disposable
            /// <summary>Set a [language configuration](#LanguageConfiguration) for a language.</summary>
            /// <param name="language">A language identifier like `typescript`.</param>
            /// <param name="configuration">Language configuration.</param>
            abstract setLanguageConfiguration: language: string * configuration: LanguageConfiguration -> Disposable

    /// Represents the input box in the Source Control viewlet.
    type [<AllowNullLiteral>] SourceControlInputBox =
        /// Setter and getter for the contents of the input box.
        abstract value: string with get, set

    type [<AllowNullLiteral>] QuickDiffProvider =
        /// <summary>Provide a [uri](#Uri) to the original resource of any given resource uri.</summary>
        /// <param name="uri">The uri of the resource open in a text editor.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideOriginalResource: uri: Uri * token: CancellationToken -> ProviderResult<Uri>

    /// The theme-aware decorations for a
    /// [source control resource state](#SourceControlResourceState).
    type [<AllowNullLiteral>] SourceControlResourceThemableDecorations =
        /// The icon path for a specific
        /// [source control resource state](#SourceControlResourceState).
        abstract iconPath: U2<string, Uri> option

    /// The decorations for a [source control resource state](#SourceControlResourceState).
    /// Can be independently specified for light and dark themes.
    type [<AllowNullLiteral>] SourceControlResourceDecorations =
        inherit SourceControlResourceThemableDecorations
        /// Whether the [source control resource state](#SourceControlResourceState) should
        /// be striked-through in the UI.
        abstract strikeThrough: bool option
        /// Whether the [source control resource state](#SourceControlResourceState) should
        /// be faded in the UI.
        abstract faded: bool option
        /// The title for a specific
        /// [source control resource state](#SourceControlResourceState).
        abstract tooltip: string option
        /// The light theme decorations.
        abstract light: SourceControlResourceThemableDecorations option
        /// The dark theme decorations.
        abstract dark: SourceControlResourceThemableDecorations option

    /// An source control resource state represents the state of an underlying workspace
    /// resource within a certain [source control group](#SourceControlResourceGroup).
    type [<AllowNullLiteral>] SourceControlResourceState =
        /// The [uri](#Uri) of the underlying resource inside the workspace.
        abstract resourceUri: Uri
        /// The [command](#Command) which should be run when the resource
        /// state is open in the Source Control viewlet.
        abstract command: Command option
        /// The [decorations](#SourceControlResourceDecorations) for this source control
        /// resource state.
        abstract decorations: SourceControlResourceDecorations option

    /// A source control resource group is a collection of
    /// [source control resource states](#SourceControlResourceState).
    type [<AllowNullLiteral>] SourceControlResourceGroup =
        /// The id of this source control resource group.
        abstract id: string
        /// The label of this source control resource group.
        abstract label: string with get, set
        /// Whether this source control resource group is hidden when it contains
        /// no [source control resource states](#SourceControlResourceState).
        abstract hideWhenEmpty: bool option with get, set
        /// This group's collection of
        /// [source control resource states](#SourceControlResourceState).
        abstract resourceStates: ResizeArray<SourceControlResourceState> with get, set
        /// Dispose this source control resource group.
        abstract dispose: unit -> unit

    /// An source control is able to provide [resource states](#SourceControlResourceState)
    /// to the editor and interact with the editor in several source control related ways.
    type [<AllowNullLiteral>] SourceControl =
        /// The id of this source control.
        abstract id: string
        /// The human-readable label of this source control.
        abstract label: string
        /// The (optional) Uri of the root of this source control.
        abstract rootUri: Uri option
        /// The [input box](#SourceControlInputBox) for this source control.
        abstract inputBox: SourceControlInputBox
        /// The UI-visible count of [resource states](#SourceControlResourceState) of
        /// this source control.
        /// 
        /// Equals to the total number of [resource state](#SourceControlResourceState)
        /// of this source control, if undefined.
        abstract count: float option with get, set
        /// An optional [quick diff provider](#QuickDiffProvider).
        abstract quickDiffProvider: QuickDiffProvider option with get, set
        /// Optional commit template string.
        /// 
        /// The Source Control viewlet will populate the Source Control
        /// input with this value when appropriate.
        abstract commitTemplate: string option with get, set
        /// Optional accept input command.
        /// 
        /// This command will be invoked when the user accepts the value
        /// in the Source Control input.
        abstract acceptInputCommand: Command option with get, set
        /// Optional status bar commands.
        /// 
        /// These commands will be displayed in the editor's status bar.
        abstract statusBarCommands: ResizeArray<Command> option with get, set
        /// Create a new [resource group](#SourceControlResourceGroup).
        abstract createResourceGroup: id: string * label: string -> SourceControlResourceGroup
        /// Dispose this source control.
        abstract dispose: unit -> unit

    module Scm =

        type [<AllowNullLiteral>] IExports =
            abstract inputBox: SourceControlInputBox
            /// <summary>Creates a new [source control](#SourceControl) instance.</summary>
            /// <param name="id">An `id` for the source control. Something short, eg: `git`.</param>
            /// <param name="label">A human-readable string for the source control. Eg: `Git`.</param>
            /// <param name="rootUri">An optional Uri of the root of the source control. Eg: `Uri.parse(workspaceRoot)`.</param>
            abstract createSourceControl: id: string * label: string * ?rootUri: Uri -> SourceControl

    /// Configuration for a debug session.
    type [<AllowNullLiteral>] DebugConfiguration =
        /// The type of the debug session.
        abstract ``type``: string with get, set
        /// The name of the debug session.
        abstract name: string with get, set
        /// The request type of the debug session.
        abstract request: string with get, set
        /// Additional debug type specific properties.
        [<Emit "$0[$1]{{=$2}}">] abstract Item: key: string -> obj option with get, set

    /// A debug session.
    type [<AllowNullLiteral>] DebugSession =
        /// The unique ID of this debug session.
        abstract id: string
        /// The debug session's type from the [debug configuration](#DebugConfiguration).
        abstract ``type``: string
        /// The debug session's name from the [debug configuration](#DebugConfiguration).
        abstract name: string
        /// Send a custom request to the debug adapter.
        abstract customRequest: command: string * ?args: obj option -> Thenable<obj option>

    /// A custom Debug Adapter Protocol event received from a [debug session](#DebugSession).
    type [<AllowNullLiteral>] DebugSessionCustomEvent =
        /// The [debug session](#DebugSession) for which the custom event was received.
        abstract session: DebugSession with get, set
        /// Type of event.
        abstract ``event``: string with get, set
        /// Event specific information.
        abstract body: obj option option with get, set

    /// A debug configuration provider allows to add the initial debug configurations to a newly created launch.json
    /// and to resolve a launch configuration before it is used to start a new debug session.
    /// A debug configuration provider is registered via #debug.registerDebugConfigurationProvider.
    type [<AllowNullLiteral>] DebugConfigurationProvider =
        /// <summary>Provides initial [debug configuration](#DebugConfiguration). If more than one debug configuration provider is
        /// registered for the same type, debug configurations are concatenated in arbitrary order.</summary>
        /// <param name="folder">The workspace folder for which the configurations are used or undefined for a folderless setup.</param>
        /// <param name="token">A cancellation token.</param>
        abstract provideDebugConfigurations: folder: WorkspaceFolder option * ?token: CancellationToken -> ProviderResult<ResizeArray<DebugConfiguration>>
        /// <summary>Resolves a [debug configuration](#DebugConfiguration) by filling in missing values or by adding/changing/removing attributes.
        /// If more than one debug configuration provider is registered for the same type, the resolveDebugConfiguration calls are chained
        /// in arbitrary order and the initial debug configuration is piped through the chain.
        /// Returning the value 'undefined' prevents the debug session from starting.</summary>
        /// <param name="folder">The workspace folder from which the configuration originates from or undefined for a folderless setup.</param>
        /// <param name="debugConfiguration">The [debug configuration](#DebugConfiguration) to resolve.</param>
        /// <param name="token">A cancellation token.</param>
        abstract resolveDebugConfiguration: folder: WorkspaceFolder option * debugConfiguration: DebugConfiguration * ?token: CancellationToken -> ProviderResult<DebugConfiguration>

    module Debug =

        type [<AllowNullLiteral>] IExports =
            /// <summary>Start debugging by using either a named launch or named compound configuration,
            /// or by directly passing a [DebugConfiguration](#DebugConfiguration).
            /// The named configurations are looked up in '.vscode/launch.json' found in the given folder.
            /// Before debugging starts, all unsaved files are saved and the launch configurations are brought up-to-date.
            /// Folder specific variables used in the configuration (e.g. '${workspaceFolder}') are resolved against the given folder.</summary>
            /// <param name="folder">The [workspace folder](#WorkspaceFolder) for looking up named configurations and resolving variables or `undefined` for a non-folder setup.</param>
            /// <param name="nameOrConfiguration">Either the name of a debug or compound configuration or a [DebugConfiguration](#DebugConfiguration) object.</param>
            abstract startDebugging: folder: WorkspaceFolder option * nameOrConfiguration: U2<string, DebugConfiguration> -> Thenable<bool>
            abstract activeDebugSession: DebugSession option
            abstract onDidChangeActiveDebugSession: Event<DebugSession option>
            abstract onDidStartDebugSession: Event<DebugSession>
            abstract onDidReceiveDebugSessionCustomEvent: Event<DebugSessionCustomEvent>
            abstract onDidTerminateDebugSession: Event<DebugSession>
            /// <summary>Register a [debug configuration provider](#DebugConfigurationProvider) for a specifc debug type.
            /// More than one provider can be registered for the same type.</summary>
            /// <param name="provider">The [debug configuration provider](#DebugConfigurationProvider) to register.</param>
            abstract registerDebugConfigurationProvider: debugType: string * provider: DebugConfigurationProvider -> Disposable

    module Extensions =

        type [<AllowNullLiteral>] IExports =
            /// <summary>Get an extension by its full identifier in the form of: `publisher.name`.</summary>
            /// <param name="extensionId">An extension identifier.</param>
            abstract getExtension: extensionId: string -> Extension<obj option> option
            /// <summary>Get an extension its full identifier in the form of: `publisher.name`.</summary>
            /// <param name="extensionId">An extension identifier.</param>
            abstract getExtension: extensionId: string -> Extension<'T> option
            abstract all: ResizeArray<Extension<obj option>>

/// Thenable is a common denominator between ES6 promises, Q, jquery.Deferred, WinJS.Promise,
/// and others. This API makes no assumption about what promise libary is being used which
/// enables reusing existing code without migrating to a specific promise implementation. Still,
/// we recommend the use of native promises which are available in this editor.
type [<AllowNullLiteral>] Thenable<'T> =
    /// <summary>Attaches callbacks for the resolution and/or rejection of the Promise.</summary>
    /// <param name="onfulfilled">The callback to execute when the Promise is resolved.</param>
    /// <param name="onrejected">The callback to execute when the Promise is rejected.</param>
    abstract ``then``: ?onfulfilled: ('T -> U2<'TResult, Thenable<'TResult>>) * ?onrejected: (obj option -> U2<'TResult, Thenable<'TResult>>) -> Thenable<'TResult>
    abstract ``then``: ?onfulfilled: ('T -> U2<'TResult, Thenable<'TResult>>) * ?onrejected: (obj option -> unit) -> Thenable<'TResult>
