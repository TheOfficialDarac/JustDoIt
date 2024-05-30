type Props = {
  name: string;
};

const Footer = ({ name }: Props) => {
  return <div className="container p-4 bg-green-300">Footer {name}</div>;
};

export default Footer;
