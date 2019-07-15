﻿module Types

open System

    type Number = int

    type Sex = Female=0|Male=1

    module Commands =
        
        type RegisterUserCommand = { FirstName: string; LastName: string; Email: string; Phone: string; Sex: Sex; Age: int; Avatar: string; Password: string }

        type LogoutCommand = { RefreshToken: string; Token: string }

        type LoginCommand = { Email: string; Password: string }

        type RefreshTokenCommand = { RefreshToken: string; JwtToken: string }

        type AddEventCommand = { Name: string; StartDate: DateTime; EndDate: DateTime; Address: string; Title: string; Description: string; MaxAttendees: int; }

        type AddTicketTemplateCommand = { Name: string; EventId: int; Price: decimal; From: DateTime; To: DateTime }

        type BuyTicketCommand = { UserEmail: string; EventId: int }

    module Responses =
        
        type AuthResponse = { JwtToken: string; RefreshToken: string; FirstName: string; LastName: string; Email: string; Role: string; }

    module StepResponses =
        
        type RegisterStepResponse = { Email: string; Password: string; JwtToken: string; RefreshToken: string; }