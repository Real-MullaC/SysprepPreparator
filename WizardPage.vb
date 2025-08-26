Public Class WizardPage

    Public Enum Page As Integer
        WelcomePage = 0
        SysCheckPage = 1
        AdvSettingsPage = 2
        SettingPreparationPage = 3
        FinishPage = 4
    End Enum

    ''' <summary>
    ''' The page of the wizard
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WizardPage As Page

    ''' <summary>
    ''' Page count
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PageCount As Integer = 5
End Class