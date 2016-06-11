<%@ Page Title="Sessions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Netmedia.DumpDay._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="ui large form segment">
        <div class="field">
            <asp:TextBox ID="TitleTextBox" runat="server" placeholder="-- title --"></asp:TextBox>
        </div>
        <div class="field">
            <asp:TextBox ID="LinkTextBox" runat="server"></asp:TextBox>
        </div>
        <asp:Button ID="SaveSessionButton" CssClass="ui positive right floated button" runat="server" Text="Add new session" OnClick="SaveSessionButton_OnClick" />
    </div>

    <asp:TextBox ID="SearchSessionsTextBox" CssClass="form-control session-search-input" placeholder="-- session search --" runat="server" OnTextChanged="SearchSessionsButton_OnTextChanged"></asp:TextBox>

    <div class="ui grid posts">

        <asp:Repeater ID="SessionsRepeater" runat="server" OnItemCommand="SessionsRepeater_OnItemCommand">
            <ItemTemplate>
                <div class="four wide column center aligned votes">
                    <div class="ui statistic">
                        <div class="value">
                            <%# Eval("Votes") %>
                        </div>
                        <div class="label">
                            Points
                        </div>
                    </div>
                </div>
                <div class="twelve wide column">
                    <a class="ui large header" href="<%# Eval("Link") %>">
                        <%# Eval("Title") %>
                    </a>
                    <div class="meta">(<%# Eval("Domain") %>)</div>
                    <ul class="ui big horizontal list voters">
                        <li class="item">
                            <asp:LinkButton ID="VoteUpButton" runat="server" CommandName="VoteUp">
                                <i class="arrow up icon"></i>vote up 
                            </asp:LinkButton>
                        </li>
                        <li class="item">
                            <asp:LinkButton ID="VoteDownButton" runat="server" CommandName="VoteDown">
                                <i class="arrow down icon"></i>vote down
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </ItemTemplate>
        </asp:Repeater>


    </div>

</asp:Content>
