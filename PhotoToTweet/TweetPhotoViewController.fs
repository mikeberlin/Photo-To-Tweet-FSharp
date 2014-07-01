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


            (*
                CIFilter *filter = [CIFilter filterWithName:@"CISepiaTone"
                              keysAndValues: kCIInputImageKey, beginImage,
                    @"inputIntensity", @0.8, nil];
                CIImage *outputImage = [filter outputImage];
                 
                // 4
                UIImage *newImage = [UIImage imageWithCIImage:outputImage];
                self.imageView.image = newImage;  
            *)

        button

    let tweetPhoto =
        let button = new UIButton(new RectangleF(173.0f, 486.0f, 125.0f, 44.0f))
        button.BackgroundColor <- UIColor.FromRGB(41, 128, 185)
        button.SetTitle("Tweet Photo", UIControlState.Normal)

        button.TouchUpInside.Add <| fun _ ->
            button.BackgroundColor <- UIColor.Orange
            // TODO: Insert built-in iOS tweet logic here...

            (*
            if (TWTweetComposeViewController.CanSendTweet) then
                // Add code below
            } else {
                // Show a message: Twitter may not be configured in Settings
            }

            var tweet = new TWTweetComposeViewController();
            tweet.SetInitialText ("Tweeting from my iOS app");

            tweet.SetCompletionHandler((TWTweetComposeViewControllerResult r) =>{
                DismissModalViewControllerAnimated(true); // hides the tweet
                if (r == TWTweetComposeViewControllerResult.Cancelled) {
                    // user cancelled the tweet
                } else {
                    // user sent the tweet (they may have edited it first)
                }
            });

            PresentModalViewController(tweet, true);

            tweet.AddUrl (new USUrl("http://xamarin.com"));

            tweet.AddImage (UIImage.FromFile("some_image.png"));

            *)

        button

    override this.ViewDidLoad() =
        base.ViewDidLoad()

        this.View <- new UIView(BackgroundColor = UIColor.FromRGB(78, 87, 88))
        this.Title <- "Edit/Tweet Photo"

        this.View.AddSubviews(photoPreview, setFilter, tweetPhoto)