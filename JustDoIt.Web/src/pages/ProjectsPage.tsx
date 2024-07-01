// import React from "react";

import { useAuth } from "../hooks/useAuth";

// interface Props {}

const ProjectsPage = () => {
  let { user } = useAuth();
  return (
    <div>
      <pre>{user.email}</pre>
    </div>
  );
};

export default ProjectsPage;
