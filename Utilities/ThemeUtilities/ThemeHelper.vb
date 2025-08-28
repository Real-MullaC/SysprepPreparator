Imports System.IO
Imports System.Text
Imports System.Drawing
Imports Microsoft.VisualBasic.ControlChars

Module ThemeHelper

    ''' <summary>
    ''' The list of loaded themes
    ''' </summary>
    ''' <remarks></remarks>
    Private _themes As New List(Of Theme)

    ''' <summary>
    ''' The currently specified theme
    ''' </summary>
    ''' <remarks></remarks>
    Public CurrentTheme As Theme

    ''' <summary>
    ''' The list of fallback themes
    ''' </summary>
    ''' <remarks>These act as default themes in case the themes folder is not present</remarks>
    Private ReadOnly FallbackThemes As New List(Of Theme)

    ''' <summary>
    ''' Loads the themes in <paramref>ThemePath</paramref>
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadThemes()
        DynaLog.LogMessage("Preparing to load themes...")
        DynaLog.LogMessage("Clearing previously loaded themes...")
        _themes.Clear()
        DynaLog.LogMessage("Adding fallback themes...")
        FallbackThemes.AddRange(New Theme() {New Theme("",
                                                       "DISMTools 0.7 Color Scheme (Dark)",
                                                       True,
                                                       ColorTranslator.FromHtml("#1F1F1F"),
                                                       ColorTranslator.FromHtml("#121212"),
                                                       Color.White,
                                                       New Color() {
                                                           ColorTranslator.FromHtml("#143A10"),
                                                           ColorTranslator.FromHtml("#246B1C"),
                                                           ColorTranslator.FromHtml("#057F1A"),
                                                           ColorTranslator.FromHtml("#085522")
                                                       }
                                            ),
                                             New Theme("",
                                                       "DISMTools 0.7 Color Scheme (Light)",
                                                       False,
                                                       ColorTranslator.FromHtml("#EEEEF2"),
                                                       ColorTranslator.FromHtml("#FCFBFF"),
                                                       Color.Black,
                                                       New Color() {
                                                           ColorTranslator.FromHtml("#C4E5C0"),
                                                           ColorTranslator.FromHtml("#6FCF97"),
                                                           ColorTranslator.FromHtml("#81E6A8"),
                                                           ColorTranslator.FromHtml("#A3F7C5")
                                                       }
                                            )
                                            }
                                        )
        DynaLog.LogMessage("Fallback themes will only be loaded. Setting them as final themes and setting the current theme...")
        _themes = FallbackThemes
        ChangeCurrentTheme(0, True)
        SetDisabledForegroundColors()
    End Sub

    ''' <summary>
    ''' Sets the foreground color for inactive items
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDisabledForegroundColors()
        For Each loadedTheme As Theme In _themes
            Dim multiplicationFactor As Decimal = If(IsLightColor(loadedTheme.ForegroundColor), 0.7, 1.3)
            Dim colorValue As Integer
            If multiplicationFactor = 1.3 Then
                colorValue = Math.Min(loadedTheme.ForegroundColor.R + 133, 255)
            Else
                colorValue = CInt(loadedTheme.ForegroundColor.R * multiplicationFactor)
            End If
            Dim disabledFgColor As Color = Color.FromArgb(
                loadedTheme.ForegroundColor.A,
                colorValue,
                colorValue,
                colorValue
                )
            loadedTheme.DisabledForegroundColor = disabledFgColor
        Next
    End Sub

    Private Function IsLightColor(color As Color) As Boolean
        Dim brightness As Double = (0.299 * color.R) + (0.587 * color.G) + (0.114 * color.B)
        Return brightness >= 128
    End Function

    ''' <summary>
    ''' Changes the current theme to one of the currently loaded ones
    ''' </summary>
    ''' <param name="themeIndex">The index of the theme to load</param>
    ''' <param name="forceDarkTheme">Determines whether to consider a dark theme, for fallback purposes</param>
    ''' <remarks>If there's an out-of-bounds exception when changing the theme, it starts taking into account <paramref>ForceDarkTheme</paramref> to
    ''' get one of the first loaded themes. If that throws an exception as well, it loads from the fallback set, whilst still taking into account <paramref>ForceDarkTheme</paramref>
    ''' </remarks>
    Sub ChangeCurrentTheme(themeIndex As Integer, Optional forceDarkTheme As Boolean = False)
        Try
            CurrentTheme = _themes(themeIndex)
        Catch ex As Exception
            Try
                CurrentTheme = _themes(If(forceDarkTheme,
                                         0,
                                         1))
            Catch ex2 As Exception
                If forceDarkTheme Then
                    CurrentTheme = FallbackThemes(0)
                Else
                    CurrentTheme = FallbackThemes(1)
                End If
            End Try
        End Try
    End Sub

    ''' <summary>
    ''' Gets a resource for a glyph based on a name
    ''' </summary>
    ''' <param name="resourceName">The name of the resource</param>
    ''' <param name="checkForDarkVariant">Determines whether to check for dark variants of a glyph.</param>
    ''' <returns>A glyph bitmap</returns>
    ''' <remarks>Setting <paramref>CheckForDarkVariant</paramref> to false can be useful for glyphs that don't adapt to color schemes</remarks>
    Function GetGlyphResource(resourceName As String, Optional checkForDarkVariant As Boolean = True) As Bitmap
        If CurrentTheme.IsDark AndAlso checkForDarkVariant Then resourceName &= "_dark"
        Dim obj As Object
        obj = My.Resources.ResourceManager.GetObject(resourceName)
        If obj Is Nothing Then
            ' Try with _light
            obj = My.Resources.ResourceManager.GetObject(resourceName & "_light")
        End If
        Return CType(obj, Bitmap)
    End Function

    ''' <summary>
    ''' Gets the list of loaded themes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetThemes() As List(Of Theme)
        Return _themes
    End Function

End Module
