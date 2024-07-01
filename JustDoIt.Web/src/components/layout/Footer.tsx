import { Listbox, ListboxItem, Radio, RadioGroup } from "@nextui-org/react";
import DummyLogo from "../../assets/icons/DummyLogo";

const Footer = () => {
  return (
    <>
      <footer className="max-w-screen-xl w-full m-4 p-4">
        <div className="border-small border-sky-300 rounded p-2 mb-2 flex flex-row items-center">
          <DummyLogo /> <p>Just Do It</p>
        </div>
        <div className="border-small border-sky-300 rounded p-2 mb-2">
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Atque libero
          aliquid molestias.
        </div>
        <div className="border-small border-sky-300 rounded p-2 mb-2">
          icons
        </div>
        <div className="flex gap-10">
          <div className="w-full border-small px-1 py-2 rounded-small border-default-200 dark:border-default-100">
            <Listbox aria-label="Listbox Variants" variant={"light"}>
              <ListboxItem key="new">New file</ListboxItem>
              <ListboxItem key="copy">Copy link</ListboxItem>
              <ListboxItem key="edit">Edit file</ListboxItem>
            </Listbox>
          </div>
          <div className="w-full border-small px-1 py-2 rounded-small border-default-200 dark:border-default-100">
            <Listbox aria-label="Listbox Variants" variant={"light"}>
              <ListboxItem key="new">New file</ListboxItem>
              <ListboxItem key="copy">Copy link</ListboxItem>
              <ListboxItem key="edit">Edit file</ListboxItem>
            </Listbox>
          </div>
          <div className="w-full px-1 py-2">
            <Listbox aria-label="Listbox Variants" variant={"light"}>
              <ListboxItem key="new">
                <strong>New file</strong>
              </ListboxItem>
              <ListboxItem key="new">New file</ListboxItem>
              <ListboxItem key="copy">Copy link</ListboxItem>
              <ListboxItem key="edit">Edit file</ListboxItem>
            </Listbox>
          </div>
        </div>
      </footer>
    </>
  );
};

export default Footer;
