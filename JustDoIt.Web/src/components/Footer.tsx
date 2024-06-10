type Props = {
  name: string;
};

const Footer = ({ name }: Props) => {
  console.log(name);
  return (
    <>
      <footer className="container bg-teal-100 p-4 mx-auto flex-initial">
        this is {name}
      </footer>
    </>
  );
};

export default Footer;
