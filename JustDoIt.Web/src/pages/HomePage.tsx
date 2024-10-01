import { Image } from "@nextui-org/react";
const Home = () => {
  return (
    <div className="flex flex-col gap-2 max-w-4xl mx-auto">
      <section className="border p-2 rounded-md grid grid-cols-2 gap-2">
        <div className="grid justify-items-center p-3">
          <Image
            height={200}
            src="https://images.unsplash.com/photo-1531403009284-440f080d1e12?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
          />
        </div>
        <div className="p-3 grid items-center">
          <p>
            Managing a project becomes a breeze with a project management app,
            which streamlines tasks, deadlines, and communication in one
            intuitive platform. You can easily assign responsibilities, track
            progress in real time, and collaborate seamlessly with team members,
            regardless of location.
          </p>
        </div>
      </section>
      <section className="border rounded p-4">
        <p>
          With built-in features like task lists, and file sharing, staying
          organized and ensuring everyone is on the same page is effortless.
          This centralized approach not only boosts productivity but also
          enhances accountability, making project management simpler and more
          efficient than ever
        </p>
      </section>
      <section className="border p-2 rounded grid grid-cols-2 px-4 items-center">
        <div className="p-3">
          <p>Turn complex project into simple task lists</p>
        </div>
        <div>
          <Image
            src="https://images.unsplash.com/photo-1507925921958-8a62f3d1a50d?q=80&w=1476&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
            height={200}
          />
        </div>
      </section>
    </div>
  );
};

export default Home;
