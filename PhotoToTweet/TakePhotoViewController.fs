namespace PhotoToTweet

open System
open System.Drawing

open MonoTouch.UIKit
open MonoTouch.Foundation

[<Register ("TakePhotoViewController")>]
type TakePhotoViewController () =
    inherit UIViewController ()

    let btnTakePhoto =
        let button = new UIButton(new RectangleF(85.0f, 150.0f, 150.0f, 44.0f))
        button.BackgroundColor <- UIColor.FromRGB(41, 128, 185)
        button.SetTitle("Take Photo", UIControlState.Normal)
        button

    let photoTaker =
        let camera = new UIImagePickerController()
        camera.SourceType <- UIImagePickerControllerSourceType.Camera
        camera.CameraDevice <- UIImagePickerControllerCameraDevice.Rear
        camera.CameraCaptureMode <- UIImagePickerControllerCameraCaptureMode.Photo
        camera.ShowsCameraControls <- true
        camera.Canceled.AddHandler(new EventHandler(fun sender eventArgs ->
            let s = sender :?> UIImagePickerController
            s.DismissViewController(true, null)
        ))
        camera

    override this.ViewDidLoad () =
        base.ViewDidLoad ()

        this.View <- new UIView(BackgroundColor = UIColor.FromRGB(78, 87, 88))
        this.Title <- "Photo To Tweet"

        btnTakePhoto.TouchUpInside.Add <| fun _ ->
            this.NavigationController.PresentViewController(photoTaker, true, null)

        photoTaker.FinishedPickingMedia.AddHandler(
            new EventHandler<UIImagePickerMediaPickedEventArgs>(
                fun sender eventArgs ->
                    let s = sender :?> UIImagePickerController

                    s.DismissViewController(false, fun _ ->
                        this.NavigationController.PushViewController(
                            new TweetPhotoViewController(eventArgs.OriginalImage), false
                        )
                    )
            )
        )

        this.View.AddSubview(btnTakePhoto)