interface Props {
  name: string;
}

const Header = ({ name }: Props) => {
  return <div className="container-xl bg-red-300 p-4">Header {name}</div>;
};

export default Header;
