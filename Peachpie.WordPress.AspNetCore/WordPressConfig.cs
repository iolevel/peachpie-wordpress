using Pchp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peachpie.WordPress.AspNetCore
{
    /// <summary>
    /// WordPress configuration.
    /// The configuration is loaded into WordPress before every request.
    /// </summary>
    public class WordPressConfig
    {
        /// <summary>MySQL database name.</summary>
        public string DbName { get; set; } = "wordpress";

        /// <summary>MySQL database user name.</summary>
        public string DbUser { get; set; } = "root";

        /// <summary>MySQL database password.</summary>
        public string DbPassword { get; set; }

        /// <summary>MySQL host.</summary>
        public string DbHost { get; set; } = "localhost";

        /// <summary>
        /// Set of WordPress plugins to be loaded.
        /// </summary>
        public IWpPlugin[] Plugins { get; set; }

        /// <summary>
        /// Defines WordPress configuration constants and initializes runtime before proceeding to <c>index.php</c>.
        /// </summary>
        public virtual void Apply(Context ctx)
        {
            // see wp-config.php:

            // The name of the database for WordPress
            ctx.DefineConstant("DB_NAME", (PhpValue)DbName); // define('DB_NAME', 'wordpress');

            // MySQL database username
            ctx.DefineConstant("DB_USER", (PhpValue)DbUser); // define('DB_USER', 'root');

            // MySQL database password
            ctx.DefineConstant("DB_PASSWORD", (PhpValue)DbPassword); // define('DB_PASSWORD', 'password');

            // MySQL hostname
            ctx.DefineConstant("DB_HOST", (PhpValue)DbHost); // define('DB_HOST', 'localhost');

            // $peachpie-wp-loader : WpLoader
            ctx.Globals["peachpie_wp_loader"] = PhpValue.FromClass(new WpLoader(Plugins));
        }
    }
}
