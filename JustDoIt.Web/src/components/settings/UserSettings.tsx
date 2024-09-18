import { Button, Image, Input } from "@nextui-org/react";
import { useAuth } from "../../hooks/useAuth";
import { useState } from "react";

const UserSettings = () => {
  const { user, authToken } = useAuth();
  console.log(user);
  let [image, setImage] = useState<string>("");

  const handleSubmit = (e) => {
    e.preventDefault();
    let pic = e.target.elements.namedItem("picture");
    console.log("PIC: ", pic.files[0]);
    console.log(URL.createObjectURL(pic.files[0]));
    setImage(() => URL.createObjectURL(pic.files[0]));
  };
  return (
    <div className="border p-2 flex flex-col">
      <h2>User Settings</h2>
      <form
        id="user-data-form"
        onSubmit={handleSubmit}
        className="flex flex-col gap-3 px-2"
      >
        {/* <Avatar
          className={"mx-auto w-1/4 h-full cursor-pointer"}
          src={user?.pictureUrl}
          //   size="lg"
          color="default"
          isBordered
          showFallback
          onClick={() => {
            alert("click");
          }}
        /> */}
        <input
          type="file"
          name="picture"
          className="hidden"
          accept="image/apng, image/bmp, image/gif, image/jpeg, image/pjpeg, image/png, image/svg+xml, image/tiff, image/webp,image/x-icon"
        />
        <Image
          removeWrapper
          className="cursor-pointer mx-auto w-64"
          //   isZoomed
          src={image ? image : user?.pictureUrl}
          onClick={() => {
            // let form = document.getElementById("user-data-form");
            // let picture = form?.children.namedItem("picture");
            const picture = document.querySelector(
              "form input[name='picture']"
            );
            picture?.click();
            // console.log(e.currentTarget.parentElement["picture"].value);
          }}
          radius="lg"
          loading="lazy"
          shadow="sm"
        />
        <Input
          isDisabled
          type="text"
          label="Username"
          defaultValue={user?.userName}
        />
        <div className="flex gap-2">
          <Input
            isDisabled
            type="text"
            label="Last Name"
            defaultValue={user?.lastName}
          />
          <Input
            isDisabled
            type="text"
            label="First Name"
            defaultValue={user?.firstName}
          />
        </div>
        <div className="flex gap-2">
          <Input
            isDisabled
            type="tel"
            label="Phone Number"
            defaultValue={user?.phoneNumber}
          />
          <Input
            isDisabled
            type="email"
            label="Email"
            defaultValue={user?.email}
          />
        </div>
        <Button type="submit">submit</Button>
      </form>
    </div>
  );
};

export default UserSettings;
