namespace PhotoToTweet

open System
open System.Drawing

open MonoTouch.UIKit
open MonoTouch.Foundation
open MonoTouch.CoreImage

open Xamarin.Social
open Xamarin.Social.Services

[<Register ("TweetPhotoViewController")>]
type TweetPhotoViewController (image: UIImage) =
    inherit UIViewController ()

    let originalImage = image
    let originalOrientation = originalImage.Orientation

    let sepiaFilter = 
        let filter = new CISepiaTone()
        filter.Image <- new CIImage(originalImage.CGImage)
        filter.Intensity <- 0.8f
        filter

    let sepiaImage =
        let ciContext = CIContext.FromOptions(null)
        new UIImage(ciContext.CreateCGImage(sepiaFilter.OutputImage, sepiaFilter.Image.Extent))

    let mutable photoPreview =
        let p = new UIImageView(new RectangleF(22.0f, 75.0f, 275.0f, 400.0f))
        p.Image <- originalImage
        p

    let setFilter =
        let button = new UIButton(new RectangleF(22.0f, 486.0f, 125.0f, 44.0f))
        button.BackgroundColor <- UIColor.FromRGB(41, 128, 185)
        button.SetTitle("Toggle Filter", UIControlState.Normal)
        photoPreview.Tag <- 0

        button.TouchUpInside.Add <| fun _ ->
            if (photoPreview.Tag = 0) then
                photoPreview.Image <- new UIImage(sepiaImage.CGImage, 1.0f, originalOrientation)
                photoPreview.Tag <- 1
                button.BackgroundColor <- UIColor.Orange
            else
                photoPreview.Image <- new UIImage(originalImage.CGImage, 1.0f, originalOrientation)
                photoPreview.Tag <- 0
                button.BackgroundColor <- UIColor.FromRGB(41, 128, 185)

        button

    let tweetPhoto =
        let button = new UIButton(new RectangleF(173.0f, 486.0f, 125.0f, 44.0f))
        button.BackgroundColor <- UIColor.FromRGB(41, 128, 185)
        button.SetTitle("Tweet Photo", UIControlState.Normal)
        button

    override this.ViewDidLoad() =
        base.ViewDidLoad()

        this.View <- new UIView(BackgroundColor = UIColor.FromRGB(78, 87, 88))
        this.Title <- "Edit/Tweet Photo"

        tweetPhoto.TouchUpInside.Add <| fun _ ->
            let tweet = new Item()
            tweet.Text <- "I'm tweeting using F#! #xamarin"
            // add a link if you want to, so many options! =)
            // tweet.Links.Add(new Uri("http://xamarin.com"))
            tweet.Images.Add(new ImageData(photoPreview.Image))

            let vcTwitter =
                let twitterService = new Twitter5Service()
                twitterService.GetShareUI(tweet, fun _ ->
                    this.DismissViewController(true, null))

            this.PresentViewController(vcTwitter, true, null)

        this.View.AddSubviews(photoPreview, setFilter, tweetPhoto)