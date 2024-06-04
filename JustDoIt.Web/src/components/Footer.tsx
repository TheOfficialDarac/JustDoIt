interface Props {
  name: string;
}

const Footer = ({ name }: Props) => {
  return <div className="p-4 bg-green-300 flex-initial">Footer {name}</div>;
};

export default Footer;
