interface Props {
  name: string;
}

const Header = ({ name }: Props) => {
  console.log(name);
  return (
    <>
      <div className="container bg-red-300 p-4 mx-auto flex-initial">
        Header {name}
      </div>
    </>
  );
};

export default Header;
