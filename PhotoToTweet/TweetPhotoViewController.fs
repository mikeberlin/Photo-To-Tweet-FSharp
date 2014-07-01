namespace PhotoToTweet

open System
open System.Drawing

open MonoTouch.UIKit
open MonoTouch.Foundation

[<Register ("TweetPhotoViewController")>]
type TweetPhotoViewController (photo: UIImage) =
    inherit UIViewController ()

    let mutable photoPreview =
        let p = new UIImageView(new RectangleF(22.0f, 75.0f, 275.0f, 400.0f))
        p.Image <- photo
        p

    let setFilter =
        let button = new UIButton(new RectangleF(22.0f, 486.0f, 125.0f, 44.0f))
        button.BackgroundColor <- UIColor.FromRGB(41, 128, 185)
        button.SetTitle("Toggle Filter", UIControlState.Normal)

        button.TouchUpInside.Add <| fun _ ->
            button.BackgroundColor <- UIColor.White
            // TODO: Insert sepia color toggle here...

        button

    let tweetPhoto =
        let button = new UIButton(new RectangleF(173.0f, 486.0f, 125.0f, 44.0f))
        button.BackgroundColor <- UIColor.FromRGB(41, 128, 185)
        button.SetTitle("Tweet Photo", UIControlState.Normal)

        button.TouchUpInside.Add <| fun _ ->
            button.BackgroundColor <- UIColor.Orange
            // TODO: Insert built-in iOS tweet logic here...

        button

    override this.ViewDidLoad() =
        base.ViewDidLoad()

        this.View <- new UIView(BackgroundColor = UIColor.FromRGB(78, 87, 88))
        this.Title <- "Edit/Tweet Photo"

        this.View.AddSubviews(photoPreview, setFilter, tweetPhoto)